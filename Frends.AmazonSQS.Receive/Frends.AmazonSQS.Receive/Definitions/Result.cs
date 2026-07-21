using Amazon.SQS.Model;
using System.Collections.Generic;

namespace Frends.AmazonSQS.Receive.Definitions;

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
    /// Length of the received content.
    /// </summary>
    /// <example></example>
    public long ContentLength { get; private set; }

    /// <summary>
    /// Status code of the operation.
    /// </summary>
    /// <example>OK</example>
    public string StatusCode { get; private set; }

    /// <summary>
    /// List of received messages.
    /// </summary>
    /// <example></example>
    public List<ReceivedMessage> Messages { get; private set; }

    /// <summary>
    /// Information about the request.
    /// </summary>
    /// <example></example>
    public SqsResponseMetadata ResponseMetadata { get; private set; }

    /// <summary>
    /// Error information.
    /// This value is generated when an exception occurs and Options.ThrowErrorOnFailure = false.
    /// </summary>
    /// <example>Error occurred...</example>
    public Error Error { get; private set; }

    internal Result(ReceiveMessageResponse response)
    {
        Success = true;
        ContentLength = response.ContentLength;
        StatusCode = response.HttpStatusCode.ToString();
        Messages = response.Messages.ConvertAll(m => new ReceivedMessage(m));
        ResponseMetadata = new SqsResponseMetadata(response.ResponseMetadata);
        Error = null;
    }

    internal Result(Error error)
    {
        Success = false;
        Error = error;
    }
}