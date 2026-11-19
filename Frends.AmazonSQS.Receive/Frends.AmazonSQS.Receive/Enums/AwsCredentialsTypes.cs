namespace Frends.AmazonSQS.Receive.Enums;

/// <summary>
/// AWS credentials types.
/// </summary>
public enum AwsCredentialsTypes
{
    /// <summary>
    /// Anonymous credentials. Using these credentials, the client does not sign the request.
    /// </summary>
    AnonymousAwsCredentials,
    /// <summary>
    /// Basic set of credentials consisting of an AccessKey and SecretKey 
    /// </summary>
    BasicAwsCredentials,
    /// <summary>
    /// Credentials that are retrieved from ConfigurationManager.AppSettings
    /// </summary>
    EnvironmentAwsCredentials,
    /// <summary>
    /// Session credentials consisting of AccessKey, SecretKey and Token 
    /// </summary>
    SessionAwsCredentials
}