using System.ComponentModel;

namespace Frends.AmazonSQS.Send.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Gets or sets a value indicating whether an error should stop the Task and throw an exception.
    /// If set to true, an exception will be thrown when an error occurs. If set to false, Task will try to continue and the error message will be added into Result.ErrorMessage and Result.Success will be set to false.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowExceptionOnError { get; set; }

    /// <summary>
    /// The length of time, in seconds, for which to delay a specific message. 
    /// Valid values: -1 to 900. Maximum: 15 minutes. 
    /// Messages with a positive value become available for processing after the delay period is finished. 
    /// If value is -1, the default value for the queue applies. 
    /// When you set FifoQueue, you can't set DelaySeconds per message. You can set this parameter only on a queue level.
    /// </summary>
    /// <example>-1</example>
    [DefaultValue(-1)]
    public int DelaySeconds { get; set; }
}