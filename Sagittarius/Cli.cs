using System.Runtime.InteropServices;

namespace Sagittarius;

public sealed class Cli {
	private readonly List<Command> _commands;
	private readonly ErrorHelper _helper;

    public Cli(List<Command> commands, TextWriter writer) {
        _commands = commands;
		_helper = new ErrorHelper(writer);
    }

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
		return command.ExecuteAsync(arguments);
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