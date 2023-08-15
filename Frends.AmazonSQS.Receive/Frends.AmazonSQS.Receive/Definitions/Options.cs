using System.ComponentModel;

namespace Frends.AmazonSQS.Receive.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
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
