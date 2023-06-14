using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// Message options
/// </summary>
/// <example>options.MessageGroupId = "group1"</example>
[DisplayName("Options")]
public class SendOptions
{
    /// <summary>
    /// The tag that specifies that a message belongs to a specific message group. (FIFO)
    /// </summary>
    /// <example>group1</example>
    [DisplayName("MessageGroupId")]
    [DefaultValue("")]
    public string MessageGroupId { get; set; }

    /// <summary>
    /// MessageDeduplicationId (FIFO)
    /// </summary>
    /// <example>deduplication1</example>
    [DisplayName("MessageDeduplicationId")]
    [DefaultValue("")]
    public string MessageDeduplicationId { get; set; }

    /// <summary>
    /// The number of seconds to delay the message from being available for processing. 
    /// </summary>
    /// <example>10</example>
    [DefaultValue(0)]
    public int DelaySeconds { get; set; }
}