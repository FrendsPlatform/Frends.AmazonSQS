﻿using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Frends.AmazonSQS.Receive.Definitions;
using Frends.AmazonSQS.Receive.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.AmazonSQS.Receive;

/// <summary>
/// Amazon S3 Task.
/// </summary>
public class AmazonSQS
{
    /// <summary>
    /// 
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.AmazonSQS.Receive)
    /// </summary>
    /// <param name="connection">Connection parameters.</param>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Optional parameters.</param>
    /// <param name="cancellationToken">Token generated by frends to stop this Task.</param>
    /// <returns>Object { long ContentLength, Object HttpStatusCode StatusCode, List { Object Message } Messages, Object ResponseMetadata ResponseMetadata }</returns>
    public static async Task<Result> Receive([PropertyTab] Connection connection, [PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        using var sqsClient = GetAmazonSQSClient(connection.UseDefaultCredentials, ConstructAWSCredentials(connection), connection.Region);

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
                cancellationToken.ThrowIfCancellationRequested();
                var deleteMessageRequest = new DeleteMessageRequest()
                {
                    QueueUrl = input.QueueUrl,
                    ReceiptHandle = message.ReceiptHandle
                };
                // https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SQS/TDeleteMessageResponse.html
                await sqsClient.DeleteMessageAsync(deleteMessageRequest);
            }
        }

        return new Result(response);
    }

    internal static AmazonSQSClient GetAmazonSQSClient(bool useDefaultCredentials, AWSCredentials awsCredentials, Region region)
    {
        // App.config or EC2 instance credentials?
        if (useDefaultCredentials)
        {
            if (region == Region.Undefined)
                return new AmazonSQSClient();
            else
                return new AmazonSQSClient(RegionSelection(region));
        }

        if (region == Region.Undefined)
            return new AmazonSQSClient(awsCredentials);
        else
            return new AmazonSQSClient(awsCredentials, RegionSelection(region));
    }

    internal static dynamic ConstructAWSCredentials(Connection connection)
    {
        if (connection.UseDefaultCredentials)
            return null;

        switch (connection.CredentialsType)
        {
            case AWSCredentialsType.BasicAWSCredentials:
                return new BasicAWSCredentials(connection.AccessKey, connection.SecretKey);
            case AWSCredentialsType.AnonymousAWSCredentials:
                return new AnonymousAWSCredentials();
            case AWSCredentialsType.EnvironmentAWSCredentials:
                return new EnvironmentVariablesAWSCredentials();
            case AWSCredentialsType.SessionAWSCredentials:
                return new SessionAWSCredentials(connection.AccessKey, connection.SecretKey, connection.SessionToken);
            default:
                throw new InvalidEnumArgumentException("Unknown credentials type.");
        }
    }

    /// <summary>
    /// Excluded from code coverage because only single region can be used with test system.
    /// </summary>
    [ExcludeFromCodeCoverage]
    private static RegionEndpoint RegionSelection(Region region)
    {
        switch (region)
        {
            case Region.EuNorth1:
                return RegionEndpoint.EUNorth1;
            case Region.EuWest1:
                return RegionEndpoint.EUWest1;
            case Region.EuWest2:
                return RegionEndpoint.EUWest2;
            case Region.EuWest3:
                return RegionEndpoint.EUWest3;
            case Region.EuCentral1:
                return RegionEndpoint.EUCentral1;
            case Region.ApSoutheast1:
                return RegionEndpoint.APSoutheast1;
            case Region.ApSoutheast2:
                return RegionEndpoint.APSoutheast2;
            case Region.ApNortheast1:
                return RegionEndpoint.APNortheast1;
            case Region.ApNortheast2:
                return RegionEndpoint.APNortheast2;
            case Region.ApSouth1:
                return RegionEndpoint.APSouth1;
            case Region.CaCentral1:
                return RegionEndpoint.CACentral1;
            case Region.CnNorth1:
                return RegionEndpoint.CNNorth1;
            case Region.CnNorthWest1:
                return RegionEndpoint.CNNorthWest1;
            case Region.SaEast1:
                return RegionEndpoint.SAEast1;
            case Region.UsEast1:
                return RegionEndpoint.USEast1;
            case Region.UsEast2:
                return RegionEndpoint.USEast2;
            case Region.UsWest1:
                return RegionEndpoint.USWest1;
            case Region.UsWest2:
                return RegionEndpoint.USWest2;
            default:
                return RegionEndpoint.EUNorth1;
        }
    }
}