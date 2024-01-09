using Amazon.Runtime;
using Amazon.SQS.Model;
using Frends.AmazonSQS.Send.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.AmazonSQS.Send.Tests;

[TestClass]
public class UnitTests
{
    private readonly string _accessKey = Environment.GetEnvironmentVariable("AWS_SQS_ACCESS_KEY_ID") ?? throw new ArgumentException("");
    private readonly string _secretKey = Environment.GetEnvironmentVariable("AWS_SQS_SECRET_ACCESS_KEY") ?? throw new ArgumentException("");
    private readonly string _queueURL = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE") ?? throw new ArgumentException("");
    private readonly Regions _region = 0;
    private Input _input;
    private Connection _connection;
    private Options _options;
    private string _msg = "";

    [TestInitialize]
    public void SetUp()
    {
        _msg = $"Frends.AmazonSQS.Send.Tests.\nDatetime:  {DateTime.Now:o}";
        _input = new Input
        {
            QueueUrl = _queueURL,
            Message = _msg
        };

        _options = new Options
        {
            DelaySeconds = -1,
            ThrowExceptionOnError = true,
        };

        _connection = new Connection
        {
            Region = _region,
            UseDefaultCredentials = false,
            CredentialsType = AWSCredentialsTypes.BasicAWSCredentials,
            AccessKey = _accessKey,
            SecretKey = _secretKey,
            SessionToken = string.Empty,
        };
    }

    [TestMethod]
    public async Task SendTest_Success()
    {
        var result = await AmazonSQS.Send(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.HttpStatus);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.ErrorMessage);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_Success_DelaySeconds_0()
    {
        _options.DelaySeconds = 0;
        var result = await AmazonSQS.Send(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.HttpStatus);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.ErrorMessage);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_Success_DelaySeconds_10()
    {
        _options.DelaySeconds = 10;
        var result = await AmazonSQS.Send(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.HttpStatus);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.ErrorMessage);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public void SendTest_Invalid_AccessKey_ThrowExceptionOnError_True()
    {
        _connection.AccessKey = "FOO";
        Assert.ThrowsExceptionAsync<Amazon.SQS.AmazonSQSException>(async () => await AmazonSQS.Send(_connection, _input, _options, default));
    }

    [TestMethod]
    public async Task SendTest_Invalid_AccessKey_ThrowExceptionOnError_False()
    {
        _connection.AccessKey = "FOO";
        _options.ThrowExceptionOnError = false;
        var result = await AmazonSQS.Send(_connection, _input, _options, default);
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.HttpStatus);
        Assert.AreEqual(0, result.ContentLength);
        Assert.AreEqual("The security token included in the request is invalid.", result.ErrorMessage.Message);
    }

    private async Task<bool> ReadTestMessage()
    {
        Thread.Sleep(1500); // Options.DelaySeconds
        using var sqsclient = AmazonSQS.GetAmazonSQSClient(false, new BasicAWSCredentials(_accessKey, _secretKey), _region);
        var request = new ReceiveMessageRequest { QueueUrl = _queueURL };
        var response = await sqsclient.ReceiveMessageAsync(request, default);
        if (response.Messages.Count > 0)
        {
            foreach (var message in response.Messages)
            {
                var deleteMessageRequest = new DeleteMessageRequest()
                {
                    QueueUrl = _queueURL,
                    ReceiptHandle = message.ReceiptHandle
                };
                await sqsclient.DeleteMessageAsync(deleteMessageRequest);
            }
            return true;
        }
        return false;
    }
}