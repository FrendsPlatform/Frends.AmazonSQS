namespace Frends.AmazonSQS.Send.Definitions;

/// <summary>
/// AWS credentials types.
/// </summary>
public enum AWSCredentialsTypes
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

/// <summary>
/// AWS regions.
/// </summary>
public enum Regions
{
#pragma warning disable CS1591 // Self explanatory.
    AfSouth1,
    ApEast1,
    ApNortheast1,
    ApNortheast2,
    ApNortheast3,
    ApSouth1,
    ApSoutheast1,
    ApSoutheast2,
    CaCentral1,
    CnNorth1,
    CnNorthWest1,
    EuCentral1,
    EuNorth1,
    EuSouth1,
    EuWest1,
    EuWest2,
    EuWest3,
    MeSouth1,
    SaEast1,
    UsEast1,
    UsEast2,
    UsWest1,
    UsWest2,
    Undefined
#pragma warning restore CS1591
}