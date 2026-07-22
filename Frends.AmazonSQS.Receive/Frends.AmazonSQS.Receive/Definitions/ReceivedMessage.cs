using System.Collections.Generic;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Represents a message received from an Amazon SQS queue.
/// </summary>
public class ReceivedMessage
{
    /// <summary>
    /// An identifier associated with the act of receiving the message.
    /// </summary>
    /// <example>AQEBwJnKyrHigUMZj6reyNu8i/LMFp...</example>
    public string ReceiptHandle { get; private set; }

    /// <summary>
    /// A unique identifier for the message.
    /// </summary>
    /// <example>06c10e77-bc60-4b45-9d5a-4a84a5f4b6a5</example>
    public string MessageId { get; private set; }

    /// <summary>
    /// An MD5 digest of the non-URL-encoded message body string.
    /// </summary>
    /// <example>fafb00f5732ab283681e124bf8747ed1</example>
    public string MD5OfBody { get; private set; }

    /// <summary>
    /// The message's contents (not URL-encoded).
    /// </summary>
    /// <example>Hello World</example>
    public string Body { get; private set; }

    /// <summary>
    /// A map of the attributes requested in ReceiveMessage to their respective values.
    /// </summary>
    /// <example>{ "SenderId": "AIDAI5YXBXQJJBV3XBVS" }</example>
    public Dictionary<string, string> Attributes { get; private set; }

    /// <summary>
    /// Each message attribute consists of a Name, Type, and Value.
    /// Values are represented as their string equivalents.
    /// </summary>
    /// <example>{ "City": "Any City" }</example>
    public Dictionary<string, string> MessageAttributes { get; private set; }

    internal ReceivedMessage(Amazon.SQS.Model.Message message)
    {
        ReceiptHandle = message.ReceiptHandle;
        MessageId = message.MessageId;
        MD5OfBody = message.MD5OfBody;
        Body = message.Body;
        Attributes = message.Attributes;

        MessageAttributes = [];
        foreach (var kvp in message.MessageAttributes)
            MessageAttributes[kvp.Key] = kvp.Value.StringValue;
    }
}
