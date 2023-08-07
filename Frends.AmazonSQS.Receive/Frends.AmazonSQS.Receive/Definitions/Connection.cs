using Frends.AmazonSQS.Receive.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Connection parameters.
/// </summary>
public class Connection
{
    /// <summary>
    /// Region selection, default EUNorth1. Undefined doesn't select region.
    /// </summary>
    /// <example>Region.EuNorth1</example>
    public Region Region { get; set; }

    /// <summary>
    /// Credentials are loaded from the application's default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance. 
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool UseDefaultCredentials { get; set; }

    /// <summary>
    /// Type of AWS Credentials. See more https://docs.aws.amazon.com/sdkfornet1/latest/apidocs/html/T_Amazon_Runtime_AWSCredentials.htm
    /// </summary>
    /// <example>AWSCredentialsType.BasicAWSCredentials</example>
    public AWSCredentialsType CredentialsType { get; set; }

    /// <summary>
    /// Access key used in AmazonSQS connection
    /// </summary>
    /// <example></example>
    [UIHint(nameof(UseDefaultCredentials), "", false)]
    [DisplayFormat(DataFormatString = "Text")]
    public string AccessKey { get; set; }

    /// <summary>
    /// Secret key used in AmazonSQS connection
    /// </summary>
    /// <example></example>
    [UIHint(nameof(UseDefaultCredentials), "", false)]
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string SecretKey { get; set; }

    /// <summary>
    /// Session token for SessionAWSCredentials.
    /// </summary>
    /// <example></example>
    [UIHint(nameof(UseDefaultCredentials), "", false)]
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string SessionToken { get; set; }
}

