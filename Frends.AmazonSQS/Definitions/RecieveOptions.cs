using System.ComponentModel;

namespace Frends.AmazonSQS.Definitions;

/// <summary>
/// Receive options
/// </summary>
[DisplayName("Options")]
public class ReceiveOptions
{
    /// <summary>
    /// Delete message(s) from queue after receiving it 
    /// </summary>
    [DefaultValue(true)]
    public bool DeleteMessageAfterReceiving { get; set; }

    /// <summary>
    /// The duration (in seconds) that the received messages are hidden from subsequent retrieve requests after being retrieved by a ReceiveMessage request. 
    /// </summary>
    [DefaultValue(30)]
    public int VisibilityTimeout { get; set; }

    /// <summary>
    /// The duration (in seconds) for which the call waits for a message to arrive in the queue before returning. If a message is available, the call returns sooner than WaitTimeSeconds. If no messages are available and the wait time expires, the call returns successfully with an empty list of messages. 
    /// </summary>
    [DefaultValue(0)]
    public int WaitTimeSeconds { get; set; }
}