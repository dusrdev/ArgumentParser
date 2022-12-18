namespace ArgumentParser;

/// <summary>
/// Command line argument parser
/// </summary>
public static class Parser {
    internal readonly static ParserExceptionMessages ExceptionMessages = new();

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
    /// Parses a string into a dictionary of arguments
    /// </summary>
    /// <param name="str"></param>
    /// <param name="commandKey">The dictionary key which will hold the command</param>
    /// <remarks>
    /// <paramref name="commandKey"/> Allows having a command and a parameter with the same name
    /// </remarks>
    /// <exception cref="ArgumentException">If str could not be parsed</exception>
    public static Dictionary<string, string> ParseArguments(string str, string commandKey = "Command") {
        var argList = Split(str);
        if (argList.Count is 0) {
            throw new ArgumentException("Count not be parsed into arguments", nameof(str));
        }
        return ParseArguments(argList, commandKey);
    }

    /// <summary>
    /// Parses a collection of strings into a dictionary of arguments
    /// </summary>
    /// <param name="args"></param>
    /// <param name="commandKey">The dictionary key which will hold the command</param>
    /// <remarks>
    /// <para><paramref name="commandKey"/> Allows having a command and a parameter with the same name</para>
    /// </remarks>
    /// <exception cref="ArgumentException">If args is empty</exception>
    public static Dictionary<string, string> ParseArguments(List<string> args, string commandKey = "Command") {
        if (args.Count is 0) {
            throw new ArgumentException("Args cannot be empty", nameof(args));
        }
        return ParseArgumentsInternal(args, commandKey);
    }

    // Parses a List<string> into a dictionary of arguments
    private static Dictionary<string, string> ParseArgumentsInternal(List<string> args, string commandKey) {
        var results = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        int i = 0;

        var first = args[i].Trim();
        // first value is a command
        if (!IsParameterName(first)) {
            results.Add(commandKey, first);
            i++;
        }

        while (i < args.Count) {
            var current = args[i].Trim();
            // Command name or similar
            if (!IsParameterName(current)) {
                results.Add(current, string.Empty);
                i++;
                continue;
            }
            // Parameter name
            int ii = 0;
            while (current[ii] == '-') {
                ii++;
            }
            // Next is unavailable or another parameter
            if (i + 1 == args.Count || IsParameterName(args[i + 1])) {
                results.Add(current[ii..], string.Empty);
                i++;
                continue;
            }
            // Next is available and not a parameter but rather a value
            var next = args[i + 1].Trim();
            results.Add(current[ii..], next);
            i += 2;
        }

        return results;
    }

    // Checks whether a string starts with "-"
    private static bool IsParameterName(ReadOnlySpan<char> str) {
        return str.StartsWith("-");
    }
}