using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// Message parameters
/// </summary>
[DisplayName("Parameters")]
public class ReceiveParameters
{
    /// <summary>
    /// Queue url.
    /// </summary>
    /// <example>https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
    public string QueueUrl { get; set; }

    /// <summary>
    /// The maximum number of messages to return. Amazon SQS never returns more messages than this value (however, fewer messages might be returned). Valid values: 1 to 10. 
    /// </summary>
    /// <example>10</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("1")]
    public int MaxNumberOfMessages { get; set; }
}