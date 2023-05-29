using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AmazonSQS.Send
{
    /// <summary>
    /// Message parameters
    /// </summary>
    [DisplayName("Parameters")]
    public class SendParameters
    {
        /// <summary>
        /// Queue url.
        /// Examples: https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
        public string QueueUrl { get; set; }

        /// <summary>
        ///  Message to be sent. Maximum size is 256kb
        ///  Examples: foo;12345
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("Hello")]
        public string Message { get; set; }
    }

    /// <summary>
    /// Message options
    /// </summary>
    [DisplayName("Options")]
    public class SendOptions
    {
        /// <summary>
        /// The tag that specifies that a message belongs to a specific message group. (FIFO)
        /// </summary>
        [DisplayName("MessageGroupId")]
        [DefaultValue("")]
        public string MessageGroupId { get; set; }

        /// <summary>
        /// MessageDeduplicationId (FIFO)
        /// </summary>
        [DisplayName("MessageDeduplicationId")]
        [DefaultValue("")]
        public string MessageDeduplicationId { get; set; }

        /// <summary>
        /// The number of seconds to delay the message from being available for processing. 
        /// </summary>
        [DefaultValue(0)]
        public int DelaySeconds { get; set; }
    }

    /// <summary>
    /// AWS Options
    /// </summary>
    public class AWSOptions
    {
        /// <summary>
        /// Region selection, default EUNorth1. Undefined doesn't select region.
        /// </summary>
        [DisplayName("Region")]
        public Regions Region { get; set; }

        /// <summary>
        /// Credentials are loaded from the application's default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance. 
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// AWSCredentials class instance. See https://docs.aws.amazon.com/sdkfornet1/latest/apidocs/html/T_Amazon_Runtime_AWSCredentials.htm
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        [PasswordPropertyText]
        public dynamic AWSCredentials { get; set; }
    }

    /// <summary>
    /// Message parameters
    /// </summary>
    [DisplayName("Parameters")]
    public class ReceiveParameters
    {
        /// <summary>
        /// Queue url.
        /// Examples: https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
        public string QueueUrl { get; set; }

        /// <summary>
        /// The maximum number of messages to return. Amazon SQS never returns more messages than this value (however, fewer messages might be returned). Valid values: 1 to 10. 
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("1")]
        public int MaxNumberOfMessages { get; set; }
    }

    /// <summary>
    /// Receive options
    /// </summary>
    [DisplayName("Options")]
    public class ReceiveOptions
    {
        /// <summary>
        /// Delete message(s) from queue after receiving it 
        /// </summary>
        [DefaultValue(true)]
        public bool DeleteMessageAfterReceiving { get; set; }

        /// <summary>
        /// The duration (in seconds) that the received messages are hidden from subsequent retrieve requests after being retrieved by a ReceiveMessage request. 
        /// </summary>
        [DefaultValue(30)]
        public int VisibilityTimeout { get; set; }

        /// <summary>
        /// The duration (in seconds) for which the call waits for a message to arrive in the queue before returning. If a message is available, the call returns sooner than WaitTimeSeconds. If no messages are available and the wait time expires, the call returns successfully with an empty list of messages. 
        /// </summary>
        [DefaultValue(0)]
        public int WaitTimeSeconds { get; set; }
    }

    /// <summary>
    /// Delete message parameters
    /// </summary>
    [DisplayName("Parameters")]
    public class DeleteParameters
    {
        /// <summary>
        /// Queue url.
        /// Examples: https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
        public string QueueUrl { get; set; }

        /// <summary>
        ///  The receipt handle associated with the message to delete. 
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        [DefaultValue("712c4c5f-982a-40c5-becb-9b7d0539734e")]
        public string ReceiptHandle { get; set; }
    }
    /// <summary>
    /// AWS Credentials parameters
    /// </summary>
    [DisplayName("Parameters")]
    public class CredentialsParameters
    {
        /// <summary>
        /// Gets or sets the access key used for authentication.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the DisplayFormat attribute to specify the data format string as "Expression".
        /// </remarks>
        [DisplayFormat(DataFormatString = "Expression")]
        public string AccessKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key used for authentication.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the DisplayFormat attribute to specify the data format string as "Expression", and the PasswordPropertyText attribute to indicate that this is a password field.
        /// </remarks>
        [DisplayFormat(DataFormatString = "Expression")]
        [PasswordPropertyText]
        public string SecretKey { get; set; }
    }
    #region Enumerations

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Regions
    {
        EUNorth1,
        EuWest1,
        EuWest2,
        EUWest3,
        EuCentral1,
        ApNortheast1,
        ApNortheast2,
        ApSouth1,
        ApSoutheast1,
        ApSoutheast2,
        CaCentral1,
        CnNorth1,
        CNNorthWest1,
        SaEast1,
        UsEast1,
        UsEast2,
        UsWest1,
        UsWest2,
        Undefined
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    
    #endregion
}