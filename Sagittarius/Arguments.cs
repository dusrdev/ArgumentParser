namespace Sagittarius;

/// <summary>
/// A wrapper class over a dictionary of string : string with additional features
/// </summary>
public class Arguments {
    private readonly Dictionary<string, string> _arguments;

    /// <summary>
    /// Internal constructor for the <see cref="Arguments"/> class
    /// </summary>
    /// <param name="arguments">Ensure not null or empty</param>
    internal Arguments(Dictionary<string, string> arguments) {
        _arguments = arguments;
    }

    /// <summary>
    /// Attempts to retrieve a value of an argument by the key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or whitespace</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public string? GetValue(string key, bool throwIfUnable = false) {
        if (!_arguments.TryGetValue(key, out string? value)) {
            if (!throwIfUnable) {
                return null;
            }
            if (key.All(char.IsDigit)) {
                throw new KeyNotFoundException($"The positional argument for position {key} wasn't found.");
            }
            throw new KeyNotFoundException($"The named argument \"{key}\" wasn't found.");
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for named argument \"{key}\" was null or whitespace.");
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
    public int? GetValueAsInteger(string key, bool throwIfUnable = false) {
        if (!_arguments.TryGetValue(key, out string? value)) {
            if (!throwIfUnable) {
                return null;
            }
            if (key.All(char.IsDigit)) {
                throw new KeyNotFoundException($"The positional argument for position {key} wasn't found.");
            }
            throw new KeyNotFoundException($"The named argument \"{key}\" wasn't found.");
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for named argument \"{key}\" was null or whitespace.");
        }
        if (!int.TryParse(value, out var result)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for argument \"{key}\" was \"{value}\" which is invalid for type:Integer.");
        }
        return result;
    }

    /// <summary>
    /// Attempts to convert a value of an argument by the key to a double
    /// </summary>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public double? GetValueAsDouble(string key, bool throwIfUnable = false) {
        if (!_arguments.TryGetValue(key, out string? value)) {
            if (!throwIfUnable) {
                return null;
            }
            if (key.All(char.IsDigit)) {
                throw new KeyNotFoundException($"The positional argument for position {key} wasn't found.");
            }
            throw new KeyNotFoundException($"The named argument \"{key}\" wasn't found.");
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for named argument \"{key}\" was null or whitespace.");
        }
        if (!double.TryParse(value, out var result)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for argument \"{key}\" was \"{value}\" which is invalid for type:Double.");
        }
        return result;
    }

    /// <summary>
    /// Attempts to convert a value of an argument by the key to a decimal
    /// </summary>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public decimal? GetValueAsDecimal(string key, bool throwIfUnable = false) {
        if (!_arguments.TryGetValue(key, out string? value)) {
            if (!throwIfUnable) {
                return null;
            }
            if (key.All(char.IsDigit)) {
                throw new KeyNotFoundException($"The positional argument for position {key} wasn't found.");
            }
            throw new KeyNotFoundException($"The named argument \"{key}\" wasn't found.");
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for named argument \"{key}\" was null or whitespace.");
        }
        if (!decimal.TryParse(value, out var result)) {
            if (!throwIfUnable) {
                return null;
            }
            throw new ArgumentException($"The value for argument \"{key}\" was \"{value}\" which is invalid for type:Decimal.");
        }
        return result;
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
    public void Validate(string key, Func<string, bool>? validationFunc = null, string? errorMessage = null) {
        if (!_arguments.TryGetValue(key, out string? value)) {
            if (key.All(char.IsDigit)) {
                throw new KeyNotFoundException($"The positional argument for position {key} wasn't found.");
            }
            throw new KeyNotFoundException($"The named argument \"{key}\" wasn't found.");
        }
        if (validationFunc is not null && !validationFunc(value)) {
            throw new ArgumentException(errorMessage, key);
        }
    }

    /// <summary>
    /// Forwards the positional arguments by 1, so that argument that was 1 is now 0, 2 is now 1 and so on
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is useful if you have a command that has a sub-command and you want to pass the arguments to the sub-command
    /// </para>
    /// <para>
    /// The first positional argument (0) will be removed
    /// </para>
    /// </remarks>
    public void ForwardPositionalArguments() {
        for (int i = 1; _arguments.TryGetValue(i.ToString(), out string? value); i++) {
            _arguments[(i - 1).ToString()] = value;
        }
    }

    /// <summary>
    /// Returns the underlying dictionary
    /// </summary>
    public Dictionary<string, string>? GetInnerDictionary() {
        return _arguments;
    }
}
