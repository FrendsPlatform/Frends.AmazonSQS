using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AmazonSQS.Send.Definitions;

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
    /// The message to send. 
    /// The maximum string size is 256 KB.
    /// A message can include only XML, JSON, and unformatted text.
    /// </summary>
    /// <example>Hello world</example>
    public string Message { get; set; }
}