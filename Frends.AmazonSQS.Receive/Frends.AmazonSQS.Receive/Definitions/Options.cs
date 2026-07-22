using System.ComponentModel;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Gets or sets a value indicating whether an error should stop the Task and throw an exception.
    /// If set to true, an exception will be thrown when an error occurs. If set to false, Task will try to continue and the error message will be added into Result.Error and Result.Success will be set to false.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Gets or sets a custom error message to be used when an error occurs and ThrowErrorOnFailure is set to false.
    /// If empty, the original error message will be used.
    /// </summary>
    /// <example>Failed to receive message from AmazonSQS.</example>
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; }

    /// <summary>
    /// Delete message(s) from queue after receiving it 
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool DeleteMessageAfterReceiving { get; set; }

    /// <summary>
    /// The duration (in seconds) that the received messages are hidden from subsequent retrieve requests after being retrieved by a ReceiveMessage request. 
    /// </summary>
    /// <example>30</example>
    [DefaultValue(30)]
    public int VisibilityTimeout { get; set; }

    /// <summary>
    /// The duration (in seconds) for which the call waits for a message to arrive in the queue before returning. If a message is available, the call returns sooner than WaitTimeSeconds. If no messages are available and the wait time expires, the call returns successfully with an empty list of messages. 
    /// </summary>
    /// <example>30</example>
    [DefaultValue(0)]
    public int WaitTimeSeconds { get; set; }
}
