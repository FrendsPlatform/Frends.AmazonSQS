namespace Frends.AmazonSQS.Send.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the Task was executed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// MessageId.
    /// </summary>
    /// <example>aae72806-1469-4663-b3d3-5228d163baa6</example>
    public string MessageId { get; private set; }

    /// <summary>
    /// HTTP status code of the operation.
    /// </summary>
    /// <example>OK</example>
    public string HttpStatus { get; private set; }

    /// <summary>
    /// Length of the sent content.
    /// </summary>
    /// <example>378</example>
    public long ContentLength { get; private set; }

    /// <summary>
    /// Error message.
    /// This value is generated when an exception occurs and Options.ThrowErrorOnFailure = false.
    /// </summary>
    /// <example>Error occured...</example>
    public dynamic ErrorMessage { get; private set; }

    internal Result(bool success, string messageId, string httpStatus, long contentLength, dynamic errorMessage)
    {
        Success = success;
        MessageId = messageId;
        HttpStatus = httpStatus;
        ContentLength = contentLength;
        ErrorMessage = errorMessage;
    }
}