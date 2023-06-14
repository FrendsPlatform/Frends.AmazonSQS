using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// AWS Options
/// </summary>
public class AWSOptions
{
    /// <summary>
    /// Region selection, default EUNorth1. Undefined doesn't select region.
    /// </summary>
    /// <example>Regions.USWest2</example>
    [DisplayName("Region")]
    public Regions Region { get; set; }

    /// <summary>
    /// Credentials are loaded from the application's default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance. 
    /// </summary>
    public bool UseDefaultCredentials { get; set; }

    /// <summary>
    /// AWSCredentials class instance. See https://docs.aws.amazon.com/sdkfornet1/latest/apidocs/html/T_Amazon_Runtime_AWSCredentials.htm
    /// </summary>
    /// <example>new BasicAWSCredentials("accessKey", "secretKey")</example>
    [DisplayFormat(DataFormatString = "Expression")]
    [PasswordPropertyText]
    public dynamic AWSCredentials { get; set; }
}