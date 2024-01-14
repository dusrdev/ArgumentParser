using System.Runtime.InteropServices;
using System.Text;

namespace Sagittarius;

/// <summary>
/// Represents a command line interface application (Not necessarily console).
/// </summary>
public sealed class Cli {
	private readonly List<Command> _commands;
	private readonly ErrorHelper _helper;
	private readonly TextWriter _writer;
	private readonly Dictionary<string, string> _metaData;
	private readonly string _help;

	/// <summary>
	/// Creates a new instance of the <see cref="Cli"/> class.
	/// </summary>
	/// <remarks>To be used with the <see cref="CliBuilder"/></remarks>
    internal Cli(List<Command> commands, TextWriter writer, Dictionary<string, string> metaData) {
        _commands = commands;
		_writer = writer;
        _helper = new ErrorHelper(_writer);
        _metaData = metaData;
		_help = CreateHelp();
    }

	/// <summary>
	/// Runs the CLI application with the specified arguments.
	/// </summary>
    public ValueTask<int> RunAsync(string args, bool commandNameRequired = true) {
		if (args.Length is 0) {
			return _helper.Throw("No command specified", 404);
		}
		var arguments = Parser.ParseArguments(args.AsSpan());
		if (arguments is null) {
			return _helper.Throw("Input could not be parsed", 400);
		}
		if (!commandNameRequired) {
			if (_commands.Count is 1) {
				if (arguments.Contains("help")) {
					_writer.WriteLine(_commands[0].Usage);
				}
				return _commands[0].ExecuteAsync(arguments);
			}
			return _helper.Throw("Command name is required when using more than one command", 405);
		}
		string commandName = arguments.GetValue("0", true);
		Command? command = null;
		foreach (Command c in CollectionsMarshal.AsSpan(_commands)) {
			if (c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase)) {
				command = c;
				break;
			}
		}
		if (command is null) {
			return _helper.Throw($"Command \"{commandName}\" not found.", 404);
		}
		if (arguments.Contains("help")) {
			_writer.WriteLine(command.Usage);
			return ValueTask.FromResult(0);
		}
		return command.ExecuteAsync(arguments.ForwardPositionalArguments());
	}

	private string CreateHelp() {
		var builder = new StringBuilder();
		if (_metaData.TryGetValue("ApplicationName", out string? name)) {
			builder.AppendLine(name)
                .AppendLine();
		}
		if (_metaData.TryGetValue("ApplicationDescription", out string? description)) {
			builder.AppendLine(description)
                .AppendLine();
		}
		if (_metaData.TryGetValue("ApplicationVersion", out string? version)) {
			builder.Append("Version: ")
                .Append(version)
				.AppendLine();
		}
		builder.AppendLine("Commands:")
            .AppendLine();
		foreach (Command command in CollectionsMarshal.AsSpan(_commands)) {
			builder.Append(command.Name)
                .Append(" - ")
                .AppendLine(command.Description);
		}
		builder
			.AppendLine("To get help for a command, use the following syntax:")
			.AppendLine()
			.AppendLine("<command> --help")
			.AppendLine()
			.AppendLine("To get help for the application, use the following syntax:")
			.AppendLine()
			.AppendLine("--help");
		return builder.ToString();
	}

	private sealed class ErrorHelper {
		private readonly TextWriter _writer;

		public ErrorHelper(TextWriter writer) {
			_writer = writer;
		}

		public ValueTask<int> Throw(string message, int code) {
			_writer.WriteLine($"{message}. Error:{code}");
			return ValueTask.FromResult(code);
		}
	}
}