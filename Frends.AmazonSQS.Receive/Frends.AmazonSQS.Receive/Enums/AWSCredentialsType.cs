namespace Frends.AmazonSQS.Receive.Enums;

/// <summary>
/// AWS credentials types.
/// </summary>
public enum AWSCredentialsType
{
    /// <summary>
    /// Anonymous credentials. Using these credentials, the client does not sign the request.
    /// </summary>
    AnonymousAWSCredentials,
    /// <summary>
    /// Basic set of credentials consisting of an AccessKey and SecretKey 
    /// </summary>
    BasicAWSCredentials,
    /// <summary>
    /// Credentials that are retrieved from ConfigurationManager.AppSettings
    /// </summary>
    EnvironmentAWSCredentials,
    /// <summary>
    /// Session credentials consisting of AccessKey, SecretKey and Token 
    /// </summary>
    SessionAWSCredentials
}