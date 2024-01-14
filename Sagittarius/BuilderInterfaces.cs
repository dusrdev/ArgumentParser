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
	WriterEnabledCliBuilder AddCommand(Command command);
	/// <summary>
	/// Adds the specified commands to the command-enabled CLI builder.
	/// </summary>
	/// <param name="commands">The commands to add.</param>
	WriterEnabledCliBuilder AddCommands(params Command[] commands);
	/// <summary>
	/// Represents a builder for a command-enabled CLI application.
	/// </summary>
	WriterEnabledCliBuilder AddCommands(ReadOnlySpan<Command> commands);
	/// <summary>
	/// Adds commands from the specified assembly to the command-enabled CLI builder.
	/// </summary>
	WriterEnabledCliBuilder AddCommandsFromAssembly(Assembly assembly);
	/// <summary>
	/// Adds commands from the executing assembly to the command-enabled CLI builder.
	/// </summary>
	WriterEnabledCliBuilder AddCommandsFromExecutingAssembly();
}

/// <summary>
/// Represents a builder for a writer-enabled CLI application.
/// </summary>
public interface WriterEnabledCliBuilder {
	/// <summary>
	/// Sets the writer for the CLI.
	/// </summary>
	/// <param name="writer"></param>
	MetaDataEnabledCliBuilder SetOutputWriter(TextWriter writer);
	/// <summary>
	/// Sets the console writer for the CLI.
	/// </summary>
	MetaDataEnabledCliBuilder UseConsoleAsOutputWriter();
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
	/// Sets the application version for the CLI.
	/// </summary>
	MetaDataEnabledCliBuilder SetApplicationVersion(string version);
	/// <summary>
	/// Builds the CLI.
	/// </summary>
	Cli Build();
}