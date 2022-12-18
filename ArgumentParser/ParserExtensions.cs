namespace ArgumentParser;

public static class ParserExtensions {
    /// <summary>
    /// Attempts to retrieve a value
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or whitespace</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static string? GetStringValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        return arguments.GetStringValue(key, throwIfUnable, Parser.ExceptionMessages);
    }

    /// <summary>
    /// Attempts to retrieve a value
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <param name="messages">Adds an option to override the exception output message</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or whitespace</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static string? GetStringValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable, ParserExceptionMessages messages) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"{messages.KeyNotFound} --> Key: \"{key}\"");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Null, key);
            }
            return null;
        }
        return value;
    }

    /// <summary>
    /// Attempts to convert a value to an integer
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static int? GetIntegerValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        return arguments.GetIntegerValue(key, throwIfUnable, Parser.ExceptionMessages);
    }

    /// <summary>
    /// Attempts to convert a value to an integer
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <param name="messages">Adds an option to override the exception output message</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static int? GetIntegerValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable, ParserExceptionMessages messages) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"{messages.KeyNotFound} --> Key: \"{key}\"");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Null, key);
            }
            return null;
        }
        if (!int.TryParse(value, out var result)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Invalid, key);
            }
            return null;
        }
        return result;
    }

    /// <summary>
    /// Attempts to convert a value to a double
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static double? GetDoubleValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        return arguments.GetDoubleValue(key, throwIfUnable, Parser.ExceptionMessages);
    }

    /// <summary>
    /// Attempts to convert a value to a double
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <param name="messages">Adds an option to override the exception output message</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static double? GetDoubleValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable, ParserExceptionMessages messages) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"{messages.KeyNotFound} --> Key: \"{key}\"");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Null, key);
            }
            return null;
        }
        if (!double.TryParse(value, out var result)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Invalid, key);
            }
            return null;
        }
        return result;
    }

    /// <summary>
    /// Attempts to convert a value to a decimal
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static decimal? GetDecimalValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        return arguments.GetDecimalValue(key, throwIfUnable, Parser.ExceptionMessages);
    }

    /// <summary>
    /// Attempts to convert a value to a decimal
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <param name="messages">Adds an option to override the exception output message</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentException">If the value was null or could not be parsed</exception>
    /// <remarks>
    /// <para>A null return value is only possible if <paramref name="throwIfUnable"/> is false</para>
    /// <para>The <paramref name="key"/> will be added either to the message or as a property to the exceptions in order to maintain detail</para>
    /// </remarks>
    public static decimal? GetDecimalValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable, ParserExceptionMessages messages) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"{messages.KeyNotFound} --> Key: \"{key}\"");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Null, key);
            }
            return null;
        }
        if (!decimal.TryParse(value, out var result)) {
            if (throwIfUnable) {
                throw new ArgumentException(messages.Invalid, key);
            }
            return null;
        }
        return result;
    }

    /// <summary>
    /// Ensures that the argument exists, otherwise it throws an exception
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <exception cref="KeyNotFoundException"></exception>
    public static void EnsureKeyExists(this Dictionary<string, string> arguments, string key) {
        if (!arguments.ContainsKey(key)) {
            throw new KeyNotFoundException($"Argument not found: \"{key}\", but is required");
        }
    }
}
