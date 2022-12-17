using System.Runtime.InteropServices;

namespace ArgumentParser;

/// <summary>
/// Command line argument parser
/// </summary>
public static class Parser {
    /// <summary>
    /// Very efficiently splits an input into an IEnumerable, respects quotes
    /// </summary>
    /// <param name="str"></param>
    public static IEnumerable<string> Split(string str) {
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
                    yield break;
                }
                yield return str[(i + 1)..nextQuote];
                i = nextQuote + 1;
                continue;
            }
            int nextSpace = str.IndexOf(' ', i);
            if (nextSpace <= 0) {
                yield return str[i..];
                i = str.Length;
                continue;
            }
            yield return str[i..nextSpace];
            i = nextSpace + 1;
        }
    }

    /// <summary>
    /// Parses a string into a dictionary of arguments
    /// </summary>
    /// <param name="str"></param>
    /// <param name="commandKey">The dictionary key which will hold the command</param>
    /// <remarks>
    /// <paramref name="commandKey"/> Allows having a command and a parameter with the same name
    /// </remarks>
    public static Dictionary<string, string> ParseArguments(string str, string commandKey = "Command") {
        return ParseArguments(Split(str), commandKey);
    }

    /// <summary>
    /// Parses a collection of strings into a dictionary of arguments
    /// </summary>
    /// <param name="args"></param>
    /// <param name="commandKey">The dictionary key which will hold the command</param>
    /// <remarks>
    /// <para><paramref name="args"/> will be enumerated, to prevent double enumeration use the Parser.Split(string) method to create the parameter</para>
    /// <para><paramref name="commandKey"/> Allows having a command and a parameter with the same name</para>
    /// </remarks>
    public static Dictionary<string, string> ParseArguments(IEnumerable<string> args, string commandKey = "Command") {
        return ParseArgumentsInternal(CollectionsMarshal.AsSpan(args.ToList()), commandKey);
    }

    // Parses a span<string> into a dictionary of arguments
    private static Dictionary<string, string> ParseArgumentsInternal(Span<string> args, string commandKey) {
        if (args.IsEmpty) {
            throw new ArgumentException("Args cannot be empty", nameof(args));
        }

        var results = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        int i = 0;

        var first = args[i].Trim();
        // first value is a command
        if (!IsParameterName(first)) {
            results.Add(commandKey, first);
            i++;
        }

        while (i < args.Length) {
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
            if (i + 1 == args.Length || IsParameterName(args[i + 1])) {
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