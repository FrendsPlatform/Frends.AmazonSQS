using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// Delete message parameters
/// </summary>
[DisplayName("Parameters")]
public class DeleteParameters
{
    /// <summary>
    /// Queue url.
    /// </summary>
    /// <example>https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
    public string QueueUrl { get; set; }

    /// <summary>
    ///  The receipt handle associated with the message to delete. 
    /// </summary>
    /// <example>712c4c5f-982a-40c5-becb-9b7d0539734e</example>
    [DisplayFormat(DataFormatString = "Expression")]
    [DefaultValue("712c4c5f-982a-40c5-becb-9b7d0539734e")]
    public string ReceiptHandle { get; set; }
}