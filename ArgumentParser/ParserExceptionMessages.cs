namespace ArgumentParser;

/// <summary>
/// A group of default messages for their corresponding exceptions
/// </summary>
public record ParserExceptionMessages {
    /// <summary>
    /// The default message for a <see cref="KeyNotFoundException"/>
    /// </summary>
    public string KeyNotFound { get; init; } = "Key was not found in arguments";

    /// <summary>
    /// The default message for a <see cref="ArgumentException"/> when the value is null or whitespace
    /// </summary>
    public string Null { get; init; } = "Value for the key was null or whitespace";

    /// <summary>
    /// The default message for a <see cref="ArgumentException"/> when the value could not be parsed
    /// </summary>
    public string Invalid { get; init; } = "Unable to parse value for the key";
}
