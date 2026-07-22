using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Frends.AmazonSQS.Receive.Definitions;
using Frends.AmazonSQS.Receive.Enums;
using Frends.AmazonSQS.Receive.Helpers;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        var result = await AmazonSQS.Receive(input, connection, options, CancellationToken.None);
        Assert.AreEqual(1, result.Messages.Count);
        Assert.That(result.Messages[0].Body.Contains(msg), result.Messages[0].Body);
    }

    [Test]
    public async Task Receive_MultipleMessages()
    {
        await SendTestMessages(5);

        var result = await AmazonSQS.Receive(input, connection, options, CancellationToken.None);
        Assert.GreaterOrEqual(result.Messages.Count, 1);
        Assert.LessOrEqual(result.Messages.Count, 5);
        Assert.That(result.Messages[0].Body.StartsWith($"{msg}: "), result.Messages[0].Body);
    }

    [Test]
    public async Task Receive_ThrowErrorOnFailure_False_InvalidQueueUrl()
    {
        input.QueueUrl = "https://sqs.invalid-region.amazonaws.com/123456789012/invalid-queue";
        options.ThrowErrorOnFailure = false;

        var result = await AmazonSQS.Receive(input, connection, options, CancellationToken.None);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsNotNull(result.Error.Message);
        Assert.IsNull(result.Messages);
    }

    [Test]
    public async Task Receive_ThrowErrorOnFailure_False_CustomErrorMessage()
    {
        input.QueueUrl = "https://sqs.invalid-region.amazonaws.com/123456789012/invalid-queue";
        options.ThrowErrorOnFailure = false;
        options.ErrorMessageOnFailure = "Custom receive error";

        var result = await AmazonSQS.Receive(input, connection, options, CancellationToken.None);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsTrue(result.Error.Message.Contains("Custom receive error"));
    }

    [Test]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_True()
    {
        var exception = new InvalidOperationException("Test exception");
        var opts = new Options { ThrowErrorOnFailure = true };

        var ex = Assert.Throws<Exception>(() => ErrorHandler.Handle(exception, opts));

        Assert.AreEqual("Test exception", ex.Message);
        Assert.IsInstanceOf<InvalidOperationException>(ex.InnerException);
    }

    [Test]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_True_CustomMessage()
    {
        var exception = new InvalidOperationException("Test exception");
        var opts = new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = "Custom error" };

        var ex = Assert.Throws<Exception>(() => ErrorHandler.Handle(exception, opts));

        Assert.AreEqual("Custom error", ex.Message);
        Assert.IsInstanceOf<InvalidOperationException>(ex.InnerException);
    }

    [Test]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_False_DefaultMessage()
    {
        var exception = new InvalidOperationException("Test exception");
        var opts = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = string.Empty };

        var result = ErrorHandler.Handle(exception, opts);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Test exception", result.Error.Message);
        Assert.AreEqual(exception, result.Error.AdditionalInfo);
    }

    [Test]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_False_CustomMessage()
    {
        var exception = new InvalidOperationException("Test exception");
        var opts = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error message" };

        var result = ErrorHandler.Handle(exception, opts);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsTrue(result.Error.Message.Contains("Custom error message"));
        Assert.AreEqual(exception, result.Error.AdditionalInfo);
    }

    [Test]
    public void ErrorHandler_Handle_OperationCanceledException_AlwaysRethrows()
    {
        var exception = new OperationCanceledException("Cancelled");
        var opts = new Options { ThrowErrorOnFailure = false };

        Assert.Throws<OperationCanceledException>(() => ErrorHandler.Handle(exception, opts));
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
