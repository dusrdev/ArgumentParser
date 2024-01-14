using System.Text;

namespace Sagittarius;

/// <summary>
/// Represents a command for a CLI application.
/// </summary>
public abstract class Command {
	/// <summary>
	/// Gets the name of the command.
	/// </summary>
	public abstract string Name { get; }
	/// <summary>
	/// Gets the description of the command.
	/// </summary>
	public abstract string Description { get; }
	/// <summary>
	/// Gets the usage of the command.
	/// </summary>
	public abstract string Usage { get; }

	/// <summary>
	/// Executes the command.
	/// </summary>
	public abstract ValueTask<int> ExecuteAsync(Arguments args);

	/// <summary>
	/// Provides a thread-local <see cref="StringBuilder"/> for use in <see cref="GetHelp"/>.
	/// </summary>
	/// <remarks>
	/// <para>Use this to avoid allocating a new <see cref="StringBuilder"/> every time <see cref="GetHelp"/> is called.</para>
	/// </remarks>
	protected ThreadLocal<StringBuilder> _builder = new(() => new StringBuilder());

	/// <summary>
	/// Gets the help for the command.
	/// </summary>
	public virtual string GetHelp() {
		StringBuilder builder = _builder.Value!;
		builder.Clear();
		builder.AppendLine()
            .Append("Command: ")
            .AppendLine(Name)
            .AppendLine()
            .Append("Description: ")
            .AppendLine(Description)
            .AppendLine()
            .Append("Usage: ")
            .AppendLine(Usage);
		return builder.ToString();
	}
}