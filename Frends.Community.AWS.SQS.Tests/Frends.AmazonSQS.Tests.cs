using Amazon.SQS.Model;
using Frends.Amazon.SQS;
using Frends.AmazonSQS.Definitions;
using NUnit.Framework;
using System;

namespace Frends.AmazonSQS.Tests;

[TestFixture]
class BasicTests
{
    private string accessKey;
    private string secretKey;
    private string queueURL;
    private Regions region;
    private CredentialsParameters credParams;

    [SetUp]
    public void Init()
    {
        accessKey = GetConfigValue("AWS.SQS.aws_access_key_id");
        secretKey = GetConfigValue("AWS.SQS.aws_secret_access_key");
        queueURL = GetConfigValue("AWS.SQS.aws_sqs_queue");
        region = (Regions)int.Parse(GetConfigValue("AWS.SQS.aws_sqs_region"));

        credParams = new CredentialsParameters
        {
            AccessKey = accessKey,
            SecretKey = secretKey
        };

    }

    /// <summary>
    /// Send a message to the FIFO queue
    /// </summary>
    [Test]
    [Order(1)]
    public void SendMessage()
    {           
        var ret = SendTestMessage();
        Assert.IsTrue(((SendMessageResponse)ret).HttpStatusCode == System.Net.HttpStatusCode.OK);
    }

    private SendMessageResponse SendTestMessage()
    {
        var input = new SendParameters
        {
            QueueUrl = queueURL,
            Message = $@"Frends.Amazon.AWS.SQS.Tests.SendMessage() test. 
Datetime: {DateTime.Now.ToString("o")}
"
        };

        var options = new SendOptions
        {
            DelaySeconds = 0,
            MessageDeduplicationId = queueURL.Contains(".fifo") ? Guid.NewGuid().ToString() : "", // FIFO, ContentBasedDeduplication disabled
            MessageGroupId = queueURL.Contains(".fifo") ? "1" : ""            // FIFO
        };

        var awsOptions = new AWSOptions
        {
            AWSCredentials = SQS.GetBasicAWSCredentials(credParams),
            UseDefaultCredentials = false,
            Region = region
        };

        return SQS.SendMessage(input, options, awsOptions, new System.Threading.CancellationToken()).Result;
    }

    [Test]
    [Order(2)]
    public void ReceiveMessage()
    {
        ReceiveMessageResponse receiveResponse = ReceiveTestMessage();

        Assert.IsTrue(receiveResponse.Messages.Count == 1);

    }

    private ReceiveMessageResponse ReceiveTestMessage()
    {
        var input = new ReceiveParameters
        {
            QueueUrl = queueURL,
            MaxNumberOfMessages = 1
        };

        var options = new ReceiveOptions
        {
            DeleteMessageAfterReceiving = true,
            VisibilityTimeout = 30,
            WaitTimeSeconds = 5
        };

        var awsOptions = new AWSOptions
        {
            AWSCredentials = SQS.GetBasicAWSCredentials(credParams),
            UseDefaultCredentials = false,
            Region = region
        };

        return SQS.ReceiveMessage(input, options, awsOptions, new System.Threading.CancellationToken()).Result;
    }

    [Test]
    [Order(3)]
    public void DeleteMessage()
    {
        var ret = SendTestMessage();
        Assert.IsTrue(ret.HttpStatusCode == System.Net.HttpStatusCode.OK);

        var rec = ReceiveTestMessage();
        Assert.IsTrue(rec.HttpStatusCode == System.Net.HttpStatusCode.OK);
        Assert.IsTrue(rec.Messages.Count > 0);

        var input = new DeleteParameters
        {
            QueueUrl = queueURL,
            ReceiptHandle = rec.Messages[0].ReceiptHandle
        };

        var awsOptions = new AWSOptions
        {
            AWSCredentials = SQS.GetBasicAWSCredentials(credParams),
            UseDefaultCredentials = false,
            Region = region
        };

        var delres = SQS.DeleteMessage(input, awsOptions, new System.Threading.CancellationToken()).Result;

        Assert.IsTrue(delres.HttpStatusCode == System.Net.HttpStatusCode.OK);
    }

    /// <summary>
    /// Get env value
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>The value of the environment variable</returns>
    /// <example>string apiKey = GetEnvValue("API_KEY");</example>
    private string GetConfigValue(string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }
}
