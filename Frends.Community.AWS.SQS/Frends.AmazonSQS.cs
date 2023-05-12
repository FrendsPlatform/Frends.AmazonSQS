using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Frends.AmazonSQS
{
    /// <summary>
    /// Provides a set of methods for working with Amazon Simple Queue Service (SQS).
    /// </summary>
    public static class SQS
    {
        static private AmazonSQSClient GetAmazonSQSClient(bool useDefaultCredentials, AWSCredentials awsCredentials, Regions region)
        {
            // App.config or EC2 instance credentials?
            if( useDefaultCredentials == true )
            {
                if( region == Regions.Undefined)
                {
                    return new AmazonSQSClient();
                } else
                {
                    return new AmazonSQSClient(RegionSelection(region));
                }
            } else
            {
                if (region == Regions.Undefined)
                {
                    return new AmazonSQSClient(awsCredentials);
                }
                else
                {
                    return new AmazonSQSClient(awsCredentials, RegionSelection(region));
                }
            }
        }

        /// <summary>
        /// Sends a message to the AWS Simple Queue Service
        /// </summary>
        /// <param name="input">Message data</param>
        /// <param name="options">Message options</param>
        /// <param name="awsOptions">AWS options</param>
        /// <param name="cancellationToken"></param>
        /// <returns>{SendMessageResponse} </returns>
        public static async Task<dynamic> SendMessage([PropertyTab]SendParameters input, [PropertyTab]SendOptions options, [PropertyTab] AWSOptions awsOptions, CancellationToken cancellationToken)
        {
            var sqsClient = GetAmazonSQSClient(awsOptions.UseDefaultCredentials, awsOptions.AWSCredentials, awsOptions.Region);

            var request = new SendMessageRequest
            {
                //MessageAttributes = new Dictionary<string, MessageAttributeValue>
                //{
                //    {
                //        "CustomAttribute", new MessageAttributeValue
                //        { DataType = "String", StringValue = "Esan testi" }
                //    }
                //},
                MessageBody = input.Message,
                QueueUrl = input.QueueUrl,             
                DelaySeconds = options.DelaySeconds,
            };
            
            // FIFO only
            if( !string.IsNullOrEmpty(options.MessageGroupId))
            {
                request.MessageGroupId = options.MessageGroupId;
            }

            // FIFO only
            if (!string.IsNullOrEmpty(options.MessageDeduplicationId))
            {
                request.MessageDeduplicationId = options.MessageDeduplicationId;
            }

            // https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TSendMessageResponse.html
            return await sqsClient.SendMessageAsync(request, cancellationToken);
        }

        /// <summary>
        /// Receives message(s) from the AWS Simple Queue Service
        /// </summary>
        /// <param name="input">Receive parameters</param>
        /// <param name="options">Receive options</param>
        /// <param name="awsOptions">AWS options</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ReceiveMessageResponse</returns>
        public static async Task<dynamic> ReceiveMessage([PropertyTab] ReceiveParameters input, [PropertyTab]ReceiveOptions options, [PropertyTab] AWSOptions awsOptions, CancellationToken cancellationToken)
        {
            var sqsClient = GetAmazonSQSClient(awsOptions.UseDefaultCredentials, awsOptions.AWSCredentials, awsOptions.Region);

            var request = new ReceiveMessageRequest
            {
                AttributeNames = new List<string>() { "All" },
                MaxNumberOfMessages = input.MaxNumberOfMessages,
                QueueUrl = input.QueueUrl,
                VisibilityTimeout = options.VisibilityTimeout,
                WaitTimeSeconds = options.WaitTimeSeconds
            };

            // https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TReceiveMessageResponse.html
            var response = await sqsClient.ReceiveMessageAsync(request, cancellationToken);

            // Remove messages after receiving?
            if (response.Messages.Count > 0 && options.DeleteMessageAfterReceiving)
            {
                foreach (var message in response.Messages)
                {
                    var deleteMessageRequest = new DeleteMessageRequest()
                    {
                        QueueUrl = input.QueueUrl,
                        ReceiptHandle = message.ReceiptHandle
                    };
                    // https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TDeleteMessageResponse.html
                    await sqsClient.DeleteMessageAsync(deleteMessageRequest);
                }
            }

            return response;
        }

        /// <summary>
        /// Delete message using ReceiptHandle
        /// </summary>
        /// <param name="input">Delete parameters</param>
        /// <param name="awsOptions">AWS options</param>
        /// <param name="cancellationToken"></param>
        /// <returns>DeleteMessageResponse</returns>
        public static async Task<dynamic> DeleteMessage([PropertyTab] DeleteParameters input, [PropertyTab] AWSOptions awsOptions, CancellationToken cancellationToken)
        {
            var sqsClient = GetAmazonSQSClient(awsOptions.UseDefaultCredentials, awsOptions.AWSCredentials, awsOptions.Region);

            var delRequest = new DeleteMessageRequest
            {
                QueueUrl = input.QueueUrl,
                ReceiptHandle = input.ReceiptHandle
            };

            return await sqsClient.DeleteMessageAsync(delRequest, cancellationToken);
        }

        /// <summary>
        /// Get basic set of credentials consisting of an AccessKey and SecretKey 
        /// </summary>
        /// <param name="options">Input</param>
        /// <returns></returns>
        public static dynamic GetBasicAWSCredentials(CredentialsParameters options)
        {
            return new BasicAWSCredentials(options.AccessKey, options.SecretKey);
        }

        private static RegionEndpoint RegionSelection(Regions region)
        {
            switch (region)
            {
                case Regions.EUNorth1:
                    return RegionEndpoint.EUNorth1;
                case Regions.EuWest1:
                    return RegionEndpoint.EUWest1;
                case Regions.EuWest2:
                    return RegionEndpoint.EUWest2;
                case Regions.EUWest3:
                    return RegionEndpoint.EUWest3;
                case Regions.EuCentral1:
                    return RegionEndpoint.EUCentral1;
                case Regions.ApSoutheast1:
                    return RegionEndpoint.APSoutheast1;
                case Regions.ApSoutheast2:
                    return RegionEndpoint.APSoutheast2;
                case Regions.ApNortheast1:
                    return RegionEndpoint.APNortheast1;
                case Regions.ApNortheast2:
                    return RegionEndpoint.APNortheast2;
                case Regions.ApSouth1:
                    return RegionEndpoint.APSouth1;
                case Regions.CaCentral1:
                    return RegionEndpoint.CACentral1;
                case Regions.CnNorth1:
                    return RegionEndpoint.CNNorth1;
                case Regions.CNNorthWest1:
                    return RegionEndpoint.CNNorthWest1;
                case Regions.SaEast1:
                    return RegionEndpoint.SAEast1;
                case Regions.UsEast1:
                    return RegionEndpoint.USEast1;
                case Regions.UsEast2:
                    return RegionEndpoint.USEast2;
                case Regions.UsWest1:
                    return RegionEndpoint.USWest1;
                case Regions.UsWest2:
                    return RegionEndpoint.USWest2;

                default:
                    return RegionEndpoint.EUNorth1;
            }
        }
    }
}

