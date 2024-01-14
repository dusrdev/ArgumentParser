using System.Buffers;
using System.Collections.Frozen;
using System.Globalization;

namespace Sagittarius;

/// <summary>
/// A wrapper class over a dictionary of string : string with additional features
/// </summary>
public sealed class Arguments {
    private static readonly SearchValues<char> Digits = SearchValues.Create("0123456789");

    private readonly Dictionary<string, string> _arguments;

    /// <summary>
    /// Internal constructor for the <see cref="Arguments"/> class
    /// </summary>
    /// <param name="arguments">Ensure not null or empty</param>
    internal Arguments(Dictionary<string, string> arguments) {
        _arguments = arguments;
    }

    /// <summary>
    /// Gets the number of arguments.
    /// </summary>
    public int Count => _arguments.Count;

    /// <summary>
    /// Checks if the specified key exists in the arguments.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key exists, false otherwise.</returns>
    public bool Contains(string key) => _arguments.ContainsKey(key);

    /// <summary>
    /// Attempts to retrieve a value of an argument by the key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public string GetValue(string key, bool throwIfUnable = false) {
        if (!_arguments.TryGetValue(key, out string? value)) {
            if (!throwIfUnable) {
                return "";
            }
            if (!key.AsSpan().ContainsAnyExcept(Digits)) {
                throw new KeyNotFoundException($"The positional argument {key} wasn't found.");
            }
            throw new KeyNotFoundException($"The named argument \"{key}\" wasn't found.");
        }
        return value;
    }

    /// <summary>
    /// Attempts to convert a value of an argument by the key to an integer
    /// </summary>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public T? GetValue<T>(string key, bool throwIfUnable = false) where T : IParsable<T> {
        var val = GetValue(key, throwIfUnable);
        if (val.Length is 0) {
            return default;
        }
        var wasParsed = T.TryParse(val, CultureInfo.CurrentCulture, out T? result);
        if (!wasParsed) {
            if (throwIfUnable) {
                throw new ArgumentException($"The value \"{val}\" for the argument \"{key}\" could not be parsed to a {typeof(T).Name}.", key);
            }
            return default;
        }
        return result;
    }

    /// <summary>
    /// Attempts to convert a value of an argument by the key to an integer
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue">If key wasn't found or parsing failed</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public T GetValue<T>(string key, T defaultValue) where T : IParsable<T> {
        string val = GetValue(key, false);
        if (val.Length is 0) {
            return defaultValue;
        }
        var wasParsed = T.TryParse(val, CultureInfo.CurrentCulture, out T? result);
        if (!wasParsed) {
            return defaultValue;
        }
        return result!;
    }

    /// <summary>
    /// Validates the value of an argument by the key
    /// </summary>
    /// <param name="key">by name (or number if positional argument)</param>
    /// <param name="validationFunc">a validation function</param>
    /// <param name="errorMessage">if the validation function returns false</param>
    /// <exception cref="KeyNotFoundException">If the key is not found</exception>
    /// <exception cref="ArgumentException">If the validation function returns false</exception>
    /// <remarks>
    /// <para>
    /// For positional arguments enter the position as a string (e.g. "0" for the first argument and so on)
    /// </para>
    /// <para>
    /// If you require the value to be non-empty you will need to apply the <paramref name="validationFunc"/>, as if it was checked by default it would throw for boolean arguments/parameters
    /// </para>
    /// </remarks>
    public void Validate(string key, Func<string, bool> validationFunc, string errorMessage = "") {
        var val = GetValue(key, true)!;
        if (!validationFunc(val)) {
            throw new ArgumentException(errorMessage, key);
        }
    }

    /// <summary>
    /// Validates the value of an argument by the key
    /// </summary>
    /// <param name="key">by name (or number if positional argument)</param>
    /// <param name="validationFunc">a validation function</param>
    /// <param name="errorMessage">if the validation function returns false</param>
    /// <exception cref="KeyNotFoundException">If the key is not found</exception>
    /// <exception cref="ArgumentException">If the validation function returns false</exception>
    /// <remarks>
    /// <para>
    /// For positional arguments enter the position as a string (e.g. "0" for the first argument and so on)
    /// </para>
    /// <para>
    /// If you require the value to be non-empty you will need to apply the <paramref name="validationFunc"/>, as if it was checked by default it would throw for boolean arguments/parameters
    /// </para>
    /// </remarks>
    public void Validate<T>(string key, Func<T, bool> validationFunc, string errorMessage = "") where T : IParsable<T> {
        var val = GetValue<T>(key, true)!;
        if (!validationFunc(val)) {
            throw new ArgumentException(errorMessage, key);
        }
    }

    /// <summary>
    /// Returns Arguments with positional arguments forwarded by 1, so that argument that was 1 is now 0, 2 is now 1 and so on
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is useful if you have a command that has a sub-command and you want to pass the arguments to the sub-command
    /// </para>
    /// <para>
    /// The first positional argument (0) will be skipped to actually forward
    /// </para>
    /// </remarks>
    public Arguments ForwardPositionalArguments() {
        if (!Contains("0")) {
            return new(_arguments);
        }
        var dict = new Dictionary<string, string>(_arguments.Comparer);
        // We start with 1 to delete the first argument
        for (int i = 1; _arguments.TryGetValue(i.ToString(), out string? value); i++) {
            dict[(i - 1).ToString()] = value;
        }
        return new(dict);
    }

    /// <summary>
    /// Returns the underlying dictionary
    /// </summary>
    public FrozenDictionary<string, string> GetInnerDictionary() => _arguments.ToFrozenDictionary();
}
