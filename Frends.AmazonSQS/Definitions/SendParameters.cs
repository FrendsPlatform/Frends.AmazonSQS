using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// Message parameters
/// </summary>
/// <example>Sending a message to a queue</example>
[DisplayName("Parameters")]
        public class SendParameters
        {
            /// <summary>
            /// Queue url.
            /// </summary>
            /// <example>https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}</example>
            [DisplayFormat(DataFormatString = "Text")]
            [DefaultValue("https://{REGION_ENDPOINT}/queue.|api-domain|/{YOUR_ACCOUNT_NUMBER}/{YOUR_QUEUE_NAME}")]
            public string QueueUrl { get; set; }

            /// <summary>
            ///  Message to be sent. Maximum size is 256kb
            /// </summary>
            /// <example>foo;12345</example>
            [DisplayFormat(DataFormatString = "Text")]
            [DefaultValue("Hello")]
            public string Message { get; set; }
        }