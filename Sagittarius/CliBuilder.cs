using System.Reflection;

namespace Sagittarius;

public sealed class CliBuilder {
	public static CliBuilder Create() => new();

	private readonly List<Command> _commands;

	private TextWriter _writer = TextWriter.Null;

	private CliBuilder() {
		_commands = new();
	}

	public CliBuilder AddCommand(Command command) {
		_commands.Add(command);
		return this;
	}

	public CliBuilder AddCommands(params Command[] commands) {
		_commands.AddRange(commands);
		return this;
	}

	public CliBuilder AddCommands(ReadOnlySpan<Command> commands) {
		_commands.AddRange(commands);
		return this;
	}

	public CliBuilder AddCommandsFromAssembly(Assembly assembly) {
		foreach (Type type in assembly.GetTypes()) {
			if (type.IsAssignableTo(typeof(Command))) {
				_commands.Add((Command)Activator.CreateInstance(type)!);
			}
		}
		return this;
	}

	public CliBuilder AddCommandsFromExecutingAssembly() {
		return AddCommandsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public CliBuilder SetWriter(TextWriter writer) {
		_writer = writer;
		return this;
	}

	public CliBuilder SetConsoleWriter() {
		_writer = Console.Out;
		return this;
	}

	public Cli Build() {
		if (_commands.Count is 0) {
			throw new InvalidOperationException("No commands were added.");
		}
		return new Cli(_commands, _writer);
	}
}