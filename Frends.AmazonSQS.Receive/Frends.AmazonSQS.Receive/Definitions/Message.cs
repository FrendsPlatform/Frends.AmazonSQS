using System.Collections.Generic;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// AmazonSQS message class.
/// </summary>
public class Message
{
    /// <summary>
    ///  A map of the attributes requested in ReceiveMessage to their respective values. Supported attributes:
    ///  ApproximateReceiveCount, ApproximateFirstReceiveTimestamp, MessageDeduplicationId, MessageGroupId, SenderId, SentTimestamp, SequenceNumber.
    ///  ApproximateFirstReceiveTimestamp and SentTimestamp are each returned as an integer representing the epoch time in milliseconds.
    /// </summary>
    public Dictionary<string, string> Attributes { get; set; }

    /// <summary>
    ///  The message's contents (not URL-encoded).
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// An MD5 digest of the non-URL-encoded message body string.
    /// </summary>
    public string MD5OfBody { get; set; }

    /// <summary>
    /// An MD5 digest of the non-URL-encoded message attribute string.
    /// You can use this attribute to verify that Amazon SQS received the message correctly.
    /// Amazon SQS URL-decodes the message before creating the MD5 digest. For information about MD5, see RFC1321. 
    /// </summary>
    public string MD5OfMessageAttributes { get; set; }

    /// <summary>
    /// A unique identifier for the message. A MessageIdis considered unique across all Amazon Web Services accounts for an extended period of time.
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    /// An identifier associated with the act of receiving the message. A new receipt handle is returned every time you receive a message.
    /// When deleting a message, you provide the last received receipt handle to delete the message. 
    /// </summary>
    public string ReceiptHandle { get; set; }

    internal Message(Amazon.SQS.Model.Message message)
    {
        Attributes = message.Attributes;
        Body = message.Body;
        MD5OfBody = message.MD5OfBody;
        MD5OfMessageAttributes = message.MD5OfMessageAttributes;
        MessageId = message.MessageId;
        ReceiptHandle = message.ReceiptHandle;
    }
}
