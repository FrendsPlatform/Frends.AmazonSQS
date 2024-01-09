using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AmazonSQS.Send.Definitions;

/// <summary>
/// Connection parameters.
/// </summary>
public class Connection
{
    /// <summary>
    /// The region to connect.
    /// </summary>
    /// <example>Regions.EuNorth1</example>
    [DefaultValue(Regions.EuNorth1)]
    public Regions Region { get; set; }

    /// <summary>
    /// Credentials are loaded from the application's default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance. 
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool UseDefaultCredentials { get; set; }

    /// <summary>
    /// Type of AWS Credentials. 
    /// See more https://docs.aws.amazon.com/sdkfornet1/latest/apidocs/html/T_Amazon_Runtime_AWSCredentials.htm
    /// </summary>
    /// <example>AWSCredentialsTypes.BasicAWSCredentials</example>
    [DefaultValue(AWSCredentialsTypes.BasicAWSCredentials)]
    public AWSCredentialsTypes CredentialsType { get; set; }

    /// <summary>
    /// Access key used in AmazonSQS connection.
    /// </summary>
    /// <example>AccessKey</example>
    [UIHint(nameof(UseDefaultCredentials), "", false)]
    [DisplayFormat(DataFormatString = "Text")]
    public string AccessKey { get; set; }

    /// <summary>
    /// Secret key used in AmazonSQS connection.
    /// </summary>
    /// <example>SecretKey</example>
    [UIHint(nameof(UseDefaultCredentials), "", false)]
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string SecretKey { get; set; }

    /// <summary>
    /// Session token for SessionAWSCredentials.
    /// </summary>
    /// <example>SessionToken</example>
    [UIHint(nameof(UseDefaultCredentials), "", false)]
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string SessionToken { get; set; }
}