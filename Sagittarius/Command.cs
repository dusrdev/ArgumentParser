namespace Sagittarius;

public abstract class Command {
	public abstract string Name { get; }
	public abstract string Description { get; }
	public abstract string Usage { get; }

	public abstract ValueTask<int> ExecuteAsync(Arguments args);
}