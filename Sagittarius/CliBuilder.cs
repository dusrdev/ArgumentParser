using System.Reflection;

namespace Sagittarius;

/// <summary>
/// Represents a builder for a CLI application.
/// </summary>
public sealed class CliBuilder : CommandEnabledCliBuilder, WriterEnabledCliBuilder, MetaDataEnabledCliBuilder {
	private readonly List<Command> _commands;

	private readonly Dictionary<string, string> _metaData;

	private TextWriter _writer = TextWriter.Null;

	internal CliBuilder() {
		_commands = new();
		_metaData = new();
	}

/// <inheritdoc/>
	public WriterEnabledCliBuilder AddCommand(Command command) {
		_commands.Add(command);
		return this;
	}

/// <inheritdoc/>
	public WriterEnabledCliBuilder AddCommands(params Command[] commands) {
		_commands.AddRange(commands);
		return this;
	}

/// <inheritdoc/>
	public WriterEnabledCliBuilder AddCommands(ReadOnlySpan<Command> commands) {
		_commands.AddRange(commands);
		return this;
	}

/// <inheritdoc/>
	public WriterEnabledCliBuilder AddCommandsFromAssembly(Assembly assembly) {
		foreach (Type type in assembly.GetTypes()) {
			if (type.IsAssignableTo(typeof(Command))) {
				_commands.Add((Command)Activator.CreateInstance(type)!);
			}
		}
		return this;
	}

/// <inheritdoc/>
	public WriterEnabledCliBuilder AddCommandsFromExecutingAssembly() {
		return AddCommandsFromAssembly(Assembly.GetExecutingAssembly());
	}

/// <inheritdoc/>
	public MetaDataEnabledCliBuilder SetOutputWriter(TextWriter writer) {
		_writer = writer;
		return this;
	}

/// <inheritdoc/>
	public MetaDataEnabledCliBuilder UseConsoleAsOutputWriter() {
		_writer = Console.Out;
		return this;
	}

/// <inheritdoc/>
	public MetaDataEnabledCliBuilder SetApplicationName(string name) {
		_metaData["ApplicationName"] = name;
		return this;
	}

/// <inheritdoc/>
	public MetaDataEnabledCliBuilder SetApplicationDescription(string description) {
		_metaData["ApplicationDescription"] = description;
		return this;
	}

/// <inheritdoc/>
	public MetaDataEnabledCliBuilder SetApplicationVersion(string version) {
		_metaData["ApplicationVersion"] = version;
		return this;
	}

/// <inheritdoc/>
	public Cli Build() {
		if (_commands.Count is 0) {
			throw new InvalidOperationException("No commands were added.");
		}
		return new Cli(_commands, _writer, _metaData);
	}
}