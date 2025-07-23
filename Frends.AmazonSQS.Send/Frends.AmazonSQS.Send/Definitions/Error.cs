namespace Frends.AmazonSQS.Send.Definitions;

/// <summary>
/// Error information.
/// </summary>
public class Error
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    /// <example>Error occurred...</example>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets additional error information.
    /// </summary>
    /// <example>Additional details about the error</example>
    public dynamic AdditionalInfo { get; set; }
}
