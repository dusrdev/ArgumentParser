namespace ArgumentParser;

public static class ParserExtensions {
    /// <summary>
    /// Attempts to convert a value to an integer
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="key"></param>
    /// <param name="throwIfUnable">Throw an appropriate exception if unable to convert the value</param>
    /// <exception cref="KeyNotFoundException">If the key was not found</exception>
    /// <exception cref="ArgumentNullException">If the value was NullOrWhiteSpace</exception>
    /// <exception cref="ArgumentException">If the value could not be parsed</exception>
    /// <remarks>
    /// A null return value is only possible if <paramref name="throwIfUnable"/> is false
    /// </remarks>
    public static int? GetIntegerValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"Unable to find key '{key}' in arguments.");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentNullException($"Value for key '{key}' was null or whitespace.");
            }
            return null;
        }
        if (!int.TryParse(value, out int result)) {
            if (throwIfUnable) {
                throw new ArgumentException($"Unable to parse value '{value}' for key '{key}' as an integer.");
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
    /// <exception cref="ArgumentNullException">If the value was NullOrWhiteSpace</exception>
    /// <exception cref="ArgumentException">If the value could not be parsed</exception>
    /// <remarks>
    /// A null return value is only possible if <paramref name="throwIfUnable"/> is false
    /// </remarks>
    public static double? GetDoubleValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"Unable to find key '{key}' in arguments.");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentNullException($"Value for key '{key}' was null or whitespace.");
            }
            return null;
        }
        if (!double.TryParse(value, out double result)) {
            if (throwIfUnable) {
                throw new ArgumentException($"Unable to parse value '{value}' for key '{key}' as a double.");
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
    /// <exception cref="ArgumentNullException">If the value was NullOrWhiteSpace</exception>
    /// <exception cref="ArgumentException">If the value could not be parsed</exception>
    /// <remarks>
    /// A null return value is only possible if <paramref name="throwIfUnable"/> is false
    /// </remarks>
    public static decimal? GetDecimalValue(this Dictionary<string, string> arguments, string key, bool throwIfUnable = false) {
        if (!arguments.TryGetValue(key, out string? value)) {
            if (throwIfUnable) {
                throw new KeyNotFoundException($"Unable to find key '{key}' in arguments.");
            }
            return null;
        }
        if (string.IsNullOrWhiteSpace(value)) {
            if (throwIfUnable) {
                throw new ArgumentNullException($"Value for key '{key}' was null or whitespace.");
            }
            return null;
        }
        if (!decimal.TryParse(value, out decimal result)) {
            if (throwIfUnable) {
                throw new ArgumentException($"Unable to parse value '{value}' for key '{key}' as an decimal.");
            }
            return null;
        }
        return result;
    }
}
