using System.Reflection;

namespace Sagittarius;

/// <summary>
/// Represents a builder for a command-enabled CLI application.
/// </summary>
public interface CommandEnabledCliBuilder {
	/// <summary>
	/// Adds a command to the CLI.
	/// </summary>
	/// <param name="command">The command to add.</param>
	CommandEnabledCliBuilder AddCommand(Command command);
	/// <summary>
	/// Adds the specified commands to the command-enabled CLI builder.
	/// </summary>
	/// <param name="commands">The commands to add.</param>
	CommandEnabledCliBuilder AddCommands(params Command[] commands);
	/// <summary>
	/// Represents a builder for a command-enabled CLI application.
	/// </summary>
	CommandEnabledCliBuilder AddCommands(ReadOnlySpan<Command> commands);
	/// <summary>
	/// Adds commands from the specified assembly to the command-enabled CLI builder.
	/// </summary>
	CommandEnabledCliBuilder AddCommandsFromAssembly(Assembly assembly);
	/// <summary>
	/// Adds commands from the executing assembly to the command-enabled CLI builder.
	/// </summary>
	CommandEnabledCliBuilder AddCommandsFromExecutingAssembly();
}

/// <summary>
/// Represents a builder for a writer-enabled CLI application.
/// </summary>
public interface WriterEnabledCliBuilder {
	/// <summary>
	/// Sets the writer for the CLI.
	/// </summary>
	/// <param name="writer"></param>
	WriterEnabledCliBuilder SetWriter(TextWriter writer);
	/// <summary>
	/// Sets the console writer for the CLI.
	/// </summary>
	WriterEnabledCliBuilder SetConsoleWriter();
}

/// <summary>
/// Represents a builder for a metadata-enabled CLI application.
/// </summary>
public interface MetaDataEnabledCliBuilder {
	/// <summary>
	/// Sets the application name for the CLI.
	/// </summary>
	MetaDataEnabledCliBuilder SetApplicationName(string name);
	/// <summary>
	/// Sets the application description for the CLI.
	/// </summary>
	MetaDataEnabledCliBuilder SetApplicationDescription(string description);
	/// <summary>
	/// Builds the CLI.
	/// </summary>
	Cli Build();
}