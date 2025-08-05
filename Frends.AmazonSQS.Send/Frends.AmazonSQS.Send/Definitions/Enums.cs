namespace Frends.AmazonSQS.Send.Definitions;

/// <summary>
/// AWS credentials types enumeration for different authentication methods.
/// </summary>
public enum AwsCredentialsTypes
{
    /// <summary>
    /// Anonymous Aws credentials for accessing public resources without authentication.
    /// Using these credentials, the client does not sign the request.
    /// </summary>
    AnonymousAwsCredentials,

    /// <summary>
    /// Basic Aws credentials using access key and secret key.
    /// Consists of an AccessKey and SecretKey for standard authentication.
    /// </summary>
    BasicAwsCredentials,

    /// <summary>
    /// Aws credentials loaded from environment variables or application configuration.
    /// Credentials are retrieved from ConfigurationManager.AppSettings or environment variables.
    /// </summary>
    EnvironmentAwsCredentials,

    /// <summary>
    /// Session Aws credentials with temporary access using session token.
    /// Consists of AccessKey, SecretKey, and SessionToken for temporary authentication.
    /// </summary>
    SessionAwsCredentials
}

/// <summary>
/// AWS regions enumeration for Amazon SQS connections.
/// </summary>
public enum Regions
{
    /// <summary>
    /// Undefined region - uses default configuration.
    /// </summary>
    Undefined,

    /// <summary>
    /// Africa (Cape Town) - af-south-1.
    /// </summary>
    AfSouth1,

    /// <summary>
    /// Asia Pacific (Hong Kong) - ap-east-1.
    /// </summary>
    ApEast1,

    /// <summary>
    /// Asia Pacific (Tokyo) - ap-northeast-1.
    /// </summary>
    ApNortheast1,

    /// <summary>
    /// Asia Pacific (Seoul) - ap-northeast-2.
    /// </summary>
    ApNortheast2,

    /// <summary>
    /// Asia Pacific (Osaka) - ap-northeast-3.
    /// </summary>
    ApNortheast3,

    /// <summary>
    /// Asia Pacific (Mumbai) - ap-south-1.
    /// </summary>
    ApSouth1,

    /// <summary>
    /// Asia Pacific (Singapore) - ap-southeast-1.
    /// </summary>
    ApSoutheast1,

    /// <summary>
    /// Asia Pacific (Sydney) - ap-southeast-2.
    /// </summary>
    ApSoutheast2,

    /// <summary>
    /// Canada (Central) - ca-central-1.
    /// </summary>
    CaCentral1,

    /// <summary>
    /// China (Beijing) - cn-north-1.
    /// </summary>
    CnNorth1,

    /// <summary>
    /// China (Ningxia) - cn-northwest-1.
    /// </summary>
    CnNorthWest1,

    /// <summary>
    /// Europe (Frankfurt) - eu-central-1.
    /// </summary>
    EuCentral1,

    /// <summary>
    /// Europe (Stockholm) - eu-north-1.
    /// </summary>
    EuNorth1,

    /// <summary>
    /// Europe (Milan) - eu-south-1.
    /// </summary>
    EuSouth1,

    /// <summary>
    /// Europe (Ireland) - eu-west-1.
    /// </summary>
    EuWest1,

    /// <summary>
    /// Europe (London) - eu-west-2.
    /// </summary>
    EuWest2,

    /// <summary>
    /// Europe (Paris) - eu-west-3.
    /// </summary>
    EuWest3,

    /// <summary>
    /// Middle East (Bahrain) - me-south-1.
    /// </summary>
    MeSouth1,

    /// <summary>
    /// South America (São Paulo) - sa-east-1.
    /// </summary>
    SaEast1,

    /// <summary>
    /// US East (N. Virginia) - us-east-1.
    /// </summary>
    UsEast1,

    /// <summary>
    /// US East (Ohio) - us-east-2.
    /// </summary>
    UsEast2,

    /// <summary>
    /// US West (N. California) - us-west-1.
    /// </summary>
    UsWest1,

    /// <summary>
    /// US West (Oregon) - us-west-2.
    /// </summary>
    UsWest2
}
