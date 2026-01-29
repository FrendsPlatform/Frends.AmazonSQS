using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Queue url.
    /// Examples: https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
    /// </summary>
    /// <example>https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
    public string QueueUrl { get; set; }

    /// <summary>
    /// The maximum number of messages to return. Amazon SQS never returns more messages than this value (however, fewer messages might be returned). Valid values: 1 to 10.
    /// Amazon does not provide an option to force an exact number of messages.
    /// </summary>
    /// <example>1</example>
    [DefaultValue(1)]
    public int MaxNumberOfMessages { get; set; }
}
