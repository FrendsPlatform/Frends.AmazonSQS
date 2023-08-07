using Amazon.Runtime;
using Amazon.SQS.Model;
using Frends.AmazonSQS.Receive.Definitions;
using Frends.AmazonSQS.Receive.Enums;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Frends.AmazonSQS.Receive.Tests;

[TestFixture]
public class UnitTests
{
    private readonly string _accessKey = Environment.GetEnvironmentVariable("AWS.SQS.aws_access_key_id") ?? throw new ArgumentException("");
    private readonly string _secretKey = Environment.GetEnvironmentVariable("AWS.SQS.aws_secret_access_key") ?? throw new ArgumentException("");
    private readonly string _queueURL = Environment.GetEnvironmentVariable("AWS.SQS.aws_sqs_queue") ?? throw new ArgumentException("");
    private readonly Region _region = (Region)int.Parse(Environment.GetEnvironmentVariable("AWS.SQS.aws_sqs_region") ?? throw new ArgumentException(""));
    
    private Input _input;
    private Connection _connection;
    private Options _options;
    private string _msg;

    [SetUp]
    public async Task SetUp()
    {
        _input = new Input
        {
            QueueUrl = _queueURL,
            MaxNumberOfMessages = 10,
        };

        _options = new Options
        {
            DeleteMessageAfterReceiving = true,
            VisibilityTimeout = 30,
            WaitTimeSeconds = 0
        };

        _connection = new Connection
        {
            Region = _region,
            UseDefaultCredentials = false,
            CredentialsType = AWSCredentialsType.BasicAWSCredentials,
            AccessKey = _accessKey,
            SecretKey = _secretKey,
            SessionToken = string.Empty,
        };

        _msg = $"Frends.AmazonSQS.Receive.Tests.SendMessage() test.\nDatetime: {DateTime.Now.ToString("o")}";
        await SendTestMessage(_msg);
    }

    [Test]
    public async Task Receive_Test()
    {
        var result = await AmazonSQS.Receive(_connection, _input, _options, default);
        Assert.AreEqual(1, result.Messages.Count);
        Assert.AreEqual(_msg, result.Messages[0].Body);
    }

    private async Task SendTestMessage(string msg)
    {
        using var sqsclient = AmazonSQS.GetAmazonSQSClient(false, new BasicAWSCredentials(_accessKey, _secretKey), _region);
        var request = new SendMessageRequest
        {
            MessageBody = msg,
            QueueUrl = _queueURL,
            DelaySeconds = 0,
        };

        await sqsclient.SendMessageAsync(request);
    }
}