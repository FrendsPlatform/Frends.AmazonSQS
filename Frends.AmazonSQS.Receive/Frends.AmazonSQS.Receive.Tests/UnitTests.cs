using Amazon.Runtime;
using Amazon.SQS.Model;
using Frends.AmazonSQS.Receive.Definitions;
using Frends.AmazonSQS.Receive.Enums;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;

namespace Frends.AmazonSQS.Receive.Tests;

[TestFixture]
public class UnitTests
{
    public UnitTests()
    {
        DotNetEnv.Env.Load();
        accessKey = Environment.GetEnvironmentVariable("AWS_SQS_ACCESS_KEY_ID") ?? throw new ArgumentException("");
        secretKey = Environment.GetEnvironmentVariable("AWS_SQS_SECRET_ACCESS_KEY") ?? throw new ArgumentException("");
        queueUrl = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE") ?? throw new ArgumentException("");

        sqsClient = AmazonSQS.GetAmazonSQSClient(false, new BasicAWSCredentials(accessKey, secretKey), Region);
    }

    private readonly string accessKey;
    private readonly string secretKey;
    private readonly string queueUrl;


    private const Region Region = 0;
    private readonly AmazonSQSClient sqsClient;
    private string fullQueueUrl;
    private string queueName;

    private Input input;
    private Connection connection;
    private Options options;
    private string msg;

    [SetUp]
    public async Task SetUp()
    {
        queueName = Guid.NewGuid().ToString();
        fullQueueUrl = $"{queueUrl}{queueName}";
        await sqsClient.CreateQueueAsync(new CreateQueueRequest { QueueName = queueName });

        input = new Input { QueueUrl = fullQueueUrl, MaxNumberOfMessages = 10, };
        options = new Options { DeleteMessageAfterReceiving = true, VisibilityTimeout = 30, WaitTimeSeconds = 0 };
        connection = new Connection
        {
            Region = Region,
            UseDefaultCredentials = false,
            CredentialsType = AWSCredentialsType.BasicAWSCredentials,
            AccessKey = accessKey,
            SecretKey = secretKey,
            SessionToken = string.Empty,
        };

        msg = "test message";
    }

    [TearDown]
    public async Task TearDown()
    {
        await sqsClient.DeleteQueueAsync(fullQueueUrl);
    }

    [Test]
    public async Task Receive_SingleMessage()
    {
        await SendTestMessages(1);

        var result = await AmazonSQS.Receive(connection, input, options, CancellationToken.None);
        Assert.AreEqual(1, result.Messages.Count);
        Assert.That(result.Messages[0].Body.Contains(msg), result.Messages[0].Body);
    }

    [Test]
    public async Task Receive_MultipleMessages()
    {
        await SendTestMessages(5);

        var result = await AmazonSQS.Receive(connection, input, options, CancellationToken.None);
        Assert.GreaterOrEqual(1, result.Messages.Count);
        Assert.LessOrEqual(5, result.Messages.Count);
        Assert.That(result.Messages[0].Body.StartsWith($"{msg}: "), result.Messages[0].Body);
    }

    private async Task SendTestMessages(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var request =
                new SendMessageRequest { MessageBody = $"{msg}: {i}", QueueUrl = fullQueueUrl, DelaySeconds = 0 };
            await sqsClient.SendMessageAsync(request);
        }
    }
}
