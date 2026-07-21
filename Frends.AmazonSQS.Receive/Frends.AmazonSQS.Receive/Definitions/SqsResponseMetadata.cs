using System.Collections.Generic;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Metadata about the Amazon SQS response.
/// </summary>
public class SqsResponseMetadata
{
    /// <summary>
    /// The request ID returned by Amazon SQS.
    /// </summary>
    /// <example>1f38571d-db95-5c35-8e49-30b3d4199562</example>
    public string RequestId { get; private set; }

    /// <summary>
    /// Additional metadata returned in the response.
    /// </summary>
    public Dictionary<string, string> Metadata { get; private set; }

    internal SqsResponseMetadata(Amazon.Runtime.ResponseMetadata responseMetadata)
    {
        RequestId = responseMetadata?.RequestId;
        Metadata = responseMetadata?.Metadata != null
            ? new Dictionary<string, string>(responseMetadata.Metadata)
            : [];
    }
}
