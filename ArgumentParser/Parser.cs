namespace ArgumentParser;

/// <summary>
/// Command line argument parser
/// </summary>
public static class Parser {
    /// <summary>
    /// Very efficiently splits an input into a List of strings, respects quotes
    /// </summary>
    /// <param name="str"></param>
    public static List<string> Split(string str) {
        List<string> args = new();
        if (string.IsNullOrWhiteSpace(str)) {
            return args;
        }
        int i = 0;
        while (i < str.Length) {
            char c = str[i];
            if (char.IsWhiteSpace(c)) {
                i++;
                continue;
            }
            if (c is '"') {
                int nextQuote = str.IndexOf('"', i + 1);
                if (nextQuote <= 0) {
                    break;
                }
                args.Add(str[(i + 1)..nextQuote]);
                i = nextQuote + 1;
                continue;
            }
            int nextSpace = str.IndexOf(' ', i);
            if (nextSpace <= 0) {
                args.Add(str[i..]);
                i = str.Length;
                continue;
            }
            args.Add(str[i..nextSpace]);
            i = nextSpace + 1;
        }
        return args;
    }

    /// <summary>
    /// Parses a string into an <see cref="Arguments"/> object
    /// </summary>
    /// <param name="str"></param>
    public static Arguments? ParseArguments(string str) {
        var argList = Split(str);
        if (argList.Count is 0) {
            return null;
        }
        return ParseArguments(argList);
    }

    /// <summary>
    /// Parses an IList of strings into an <see cref="Arguments"/> object
    /// </summary>
    /// <param name="args"></param>
    public static Arguments? ParseArguments(IList<string> args) {
        if (args.Count is 0) {
            return null;
        }
        return ParseArgumentsInternal(args);
    }

    /// <summary>
    /// Parses an IEnumerable of strings into an <see cref="Arguments"/> object
    /// </summary>
    /// <param name="args"></param>
    public static Arguments? ParseArguments(IEnumerable<string> args) {
        var argList = args.ToList();
        if (argList is null or { Count: 0 }) {
            return null;
        }
        return ParseArgumentsInternal(argList);
    }

    // Parses a List<string> into a dictionary of arguments
    private static Arguments? ParseArgumentsInternal(IList<string> args) {
        var results = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        int i = 0;

        while (i < args.Count && !IsParameterName(args[i])) {
            results.Add(i.ToString(), args[i]);
            i++;
        }

        while (i < args.Count) {
            var current = args[i];
            // Ignore string as it is invalid parameter name
            if (!IsParameterName(current)) {
                i++;
                continue;
            }
            // Parameter name
            int ii = 0;
            while (current[ii] is '-') {
                ii++;
            }
            // Next is unavailable or another parameter
            if (i + 1 == args.Count || IsParameterName(args[i + 1])) {
                results.Add(current[ii..], string.Empty);
                i++;
                continue;
            }
            // Next is available and not a parameter but rather a value
            results.Add(current[ii..], args[i + 1]);
            i += 2;
        }

        return results.Count is 0 ? null : new Arguments(results);
    }

    // Checks whether a string starts with "-"
    private static bool IsParameterName(ReadOnlySpan<char> str) {
        return str.StartsWith("-");
    }
}