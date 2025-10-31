using System.Collections.Generic;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Response metadata class.
/// </summary>
public class ResponseMetadata
{
    /// <summary>
    /// Map of response metadata.
    /// </summary>
    public IDictionary<string, string> Metadata { get; set; }

    /// <summary>
    ///  ID that uniquely identifies a request. Amazon keeps track of request IDs. If you have a question about a request, include the request ID in your correspondence.
    /// </summary>
    public string RequestId { get; set; }

    internal ResponseMetadata(Amazon.Runtime.ResponseMetadata metadata)
    {
        Metadata = metadata.Metadata;
        RequestId = metadata.RequestId;
    }
}
