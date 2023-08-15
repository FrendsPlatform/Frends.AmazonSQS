using Amazon.Runtime;
using Amazon.SQS.Model;
using System.Collections.Generic;
using System.Net;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Length of the received content.
    /// </summary>
    /// <example></example>
    public long ContentLength { get; private set; }

    /// <summary>
    /// Status code of the operation.
    /// </summary>
    /// <example></example>
    public HttpStatusCode StatusCode { get; private set; }

    /// <summary>
    /// List of received messages.
    /// </summary>
    /// <example></example>
    public List<Message> Messages { get; private set; }

    /// <summary>
    /// Information about the request.
    /// </summary>
    /// <example></example>
    public ResponseMetadata ResponseMetadata { get; private set; }

    internal Result(ReceiveMessageResponse response)
    {
        ContentLength = response.ContentLength;
        StatusCode = response.HttpStatusCode;
        Messages = response.Messages;
        ResponseMetadata = response.ResponseMetadata;
    }
}