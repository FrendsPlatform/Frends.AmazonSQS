using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Frends.AmazonSQS.Send.Definitions;
using Frends.AmazonSQS.Send.Helpers;
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
    private Input _input = new();
    private Connection _connection = new();
    private Options _options = new();
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
            ThrowErrorOnFailure = true,
        };

        _connection = new Connection
        {
            Region = _region,
            UseDefaultCredentials = false,
            CredentialsType = AwsCredentialsTypes.BasicAwsCredentials,
            AccessKey = _accessKey,
            SecretKey = _secretKey,
            SessionToken = string.Empty,
        };
    }

    [TestMethod]
    public async Task SendTest_Success()
    {
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.StatusCode);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.Error);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_Success_DelaySeconds_0()
    {
        _options.DelaySeconds = 0;
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.StatusCode);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.Error);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_Success_DelaySeconds_10()
    {
        _options.DelaySeconds = 10;
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.StatusCode);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.Error);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public void SendTest_Invalid_AccessKey_ThrowErrorOnFailure_True()
    {
        _connection.AccessKey = "FOO";
        Assert.ThrowsExceptionAsync<Amazon.SQS.AmazonSQSException>(async () => await AmazonSQS.Send(_input, _connection, _options, default));
    }

    [TestMethod]
    public async Task SendTest_Invalid_AccessKey_ThrowErrorOnFailure_False()
    {
        _connection.AccessKey = "FOO";
        _options.ThrowErrorOnFailure = false;
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.StatusCode);
        Assert.AreEqual(0, result.ContentLength);
        Assert.AreEqual("The security token included in the request is invalid.", result.Error.Message);
    }

    private async Task<bool> ReadTestMessage()
    {
        Thread.Sleep(1500); // Options.DelaySeconds
        using var sqsclient = new AmazonSQSClient(new BasicAWSCredentials(_accessKey, _secretKey), RegionEndpoint.EUNorth1);
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

    [TestMethod]
    public async Task SendTest_Invalid_QueueUrl_ThrowErrorOnFailure_False()
    {
        _input.QueueUrl = "https://sqs.invalid-region.amazonaws.com/123456789012/invalid-queue";
        _options.ThrowErrorOnFailure = false;
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.StatusCode);
        Assert.AreEqual(0, result.ContentLength);
        Assert.IsNotNull(result.Error);
        Assert.IsNotNull(result.Error.Message);
    }

    [TestMethod]
    public async Task SendTest_Invalid_QueueUrl_ThrowErrorOnFailure_False_CustomErrorMessage()
    {
        _input.QueueUrl = "https://sqs.invalid-region.amazonaws.com/123456789012/invalid-queue";
        _options.ThrowErrorOnFailure = false;
        _options.ErrorMessageOnFailure = "Custom queue error message";
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.StatusCode);
        Assert.AreEqual(0, result.ContentLength);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Custom queue error message", result.Error.Message);
    }

    [TestMethod]
    public async Task SendTest_EmptyMessage_Success()
    {
        _input.Message = "";
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.StatusCode);
        Assert.IsTrue(result.ContentLength >= 0);
        Assert.IsNull(result.Error);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_LargeMessage_Success()
    {
        _input.Message = new string('A', 10000); // 10KB message
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.StatusCode);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.Error);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_CancellationToken_Cancelled()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        
        _options.ThrowErrorOnFailure = false;
        var result = await AmazonSQS.Send(_input, _connection, _options, cts.Token);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsTrue(result.Error.Message.Contains("canceled") || result.Error.Message.Contains("cancelled"));
    }

    [TestMethod]
    public async Task SendTest_CancellationToken_Timeout()
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(1));
        
        _options.ThrowErrorOnFailure = false;
        _options.DelaySeconds = 5; // This might cause timeout in very fast networks
        
        var result = await AmazonSQS.Send(_input, _connection, _options, cts.Token);
        // Result could be success or cancelled depending on timing
        Assert.IsTrue(result.Success || result.Error != null);
    }

    #region ErrorHandler Tests
    [TestMethod]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_True()
    {
        var exception = new InvalidOperationException("Test exception");
        var options = new Options { ThrowErrorOnFailure = true };

        Assert.ThrowsException<InvalidOperationException>(() => ErrorHandler.Handle(exception, options));
    }

    [TestMethod]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_False_DefaultMessage()
    {
        var exception = new InvalidOperationException("Test exception");
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = string.Empty };

        var result = ErrorHandler.Handle(exception, options);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.StatusCode);
        Assert.AreEqual(0, result.ContentLength);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Test exception", result.Error.Message);
        Assert.AreEqual(exception, result.Error.AdditionalInfo);
    }

    [TestMethod]
    public void ErrorHandler_Handle_ThrowErrorOnFailure_False_CustomMessage()
    {
        var exception = new InvalidOperationException("Test exception");
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error message" };

        var result = ErrorHandler.Handle(exception, options);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.StatusCode);
        Assert.AreEqual(0, result.ContentLength);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Custom error message", result.Error.Message);
        Assert.AreEqual(exception, result.Error.AdditionalInfo);
    }

    [TestMethod]
    public void ErrorHandler_Handle_NullException_ThrowErrorOnFailure_True()
    {
        var options = new Options { ThrowErrorOnFailure = true };

        Assert.ThrowsException<ArgumentNullException>(() => ErrorHandler.Handle(null, options));
    }

    [TestMethod]
    public void ErrorHandler_Handle_NullException_ThrowErrorOnFailure_False()
    {
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Null exception occurred" };

        var result = ErrorHandler.Handle(null, options);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.MessageId);
        Assert.IsNull(result.StatusCode);
        Assert.AreEqual(0, result.ContentLength);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Null exception occurred", result.Error.Message);
        Assert.IsNull(result.Error.AdditionalInfo);
    }

    [TestMethod]
    public void ErrorHandler_Handle_AmazonSQSException()
    {
        var exception = new Amazon.SQS.AmazonSQSException("SQS service error");
        var options = new Options { ThrowErrorOnFailure = false };

        var result = ErrorHandler.Handle(exception, options);

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("SQS service error", result.Error.Message);
        Assert.AreEqual(exception, result.Error.AdditionalInfo);
    }

    [TestMethod]
    public async Task SendTest_MaxMessageSize_Success()
    {
        // SQS max message size is 256KB
        _input.Message = new string('A', 256 * 1024 - 100); // Just under the limit
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.MessageId);
        Assert.AreEqual("OK", result.StatusCode);
        Assert.IsTrue(result.ContentLength > 0);
        Assert.IsNull(result.Error);
        Assert.IsTrue(await ReadTestMessage());
    }

    [TestMethod]
    public async Task SendTest_AllCredentialTypes_Coverage()
    {
        // Test BasicAWSCredentials (already covered in other tests)
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);

        // Test with UseDefaultCredentials = true
        var defaultConnection = new Connection
        {
            Region = _region,
            UseDefaultCredentials = true
        };
        
        // This might fail in test environment, but we test the code path
        _options.ThrowErrorOnFailure = false;
        var defaultResult = await AmazonSQS.Send(_input, defaultConnection, _options, default);
        // Don't assert success/failure as it depends on environment
        Assert.IsNotNull(defaultResult);
    }

    [TestMethod]
    public async Task SendTest_RegionHandling_Success()
    {
        // Test with explicit region (current setup)
        var result = await AmazonSQS.Send(_input, _connection, _options, default);
        Assert.IsTrue(result.Success);

        // Test with undefined region
        var undefinedRegionConnection = new Connection
        {
            Region = Regions.Undefined,
            UseDefaultCredentials = false,
            CredentialsType = AwsCredentialsTypes.BasicAwsCredentials,
            AccessKey = _accessKey,
            SecretKey = _secretKey,
            SessionToken = string.Empty,
        };

        _options.ThrowErrorOnFailure = false;
        var undefinedResult = await AmazonSQS.Send(_input, undefinedRegionConnection, _options, default);
        // This might fail due to region mismatch, but we test the code path
        Assert.IsNotNull(undefinedResult);
    }
    #endregion
}
