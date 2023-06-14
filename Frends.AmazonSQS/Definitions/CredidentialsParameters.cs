using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// AWS Credentials parameters
/// </summary>
[DisplayName("Parameters")]
public class CredentialsParameters
{
    /// <summary>
    /// Gets or sets the access key.
    /// </summary>
    /// <example>"AKI*********" </example>
    [DisplayFormat(DataFormatString = "Expression")]
    public string AccessKey { get; set; }

    /// <summary>
    /// Gets or sets the secret key.
    /// </summary>
    /// <example>"ABC*********XYZ"</example>
    [DisplayFormat(DataFormatString = "Expression")]
    [PasswordPropertyText]
    public string SecretKey { get; set; }
}