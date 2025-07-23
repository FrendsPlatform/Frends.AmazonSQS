using Frends.AmazonSQS.Send.Definitions;
using System;

namespace Frends.AmazonSQS.Send.Helpers;

/// <summary>
/// Static helper class for handling errors in AmazonSQS operations.
/// </summary>
public static class ErrorHandler
{
    /// <summary>
    /// Handles exceptions based on the provided options configuration.
    /// </summary>
    /// <param name="exception">The exception that occurred during the operation.</param>
    /// <param name="options">Options containing error handling configuration.</param>
    /// <returns>A Result object with error information, or throws the exception if ThrowErrorOnFailure is true.</returns>
    /// <exception cref="Exception">Throws the original exception if options.ThrowErrorOnFailure is true.</exception>
    public static Result Handle(Exception exception, Options options)
    {
        if (options.ThrowErrorOnFailure)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));
            throw exception;
        }

        var errorMessage = string.IsNullOrEmpty(options.ErrorMessageOnFailure) 
            ? (exception?.Message ?? "Unknown error occurred") 
            : options.ErrorMessageOnFailure;
        
        var error = new Error
        {
            Message = errorMessage,
            AdditionalInfo = exception
        };
        return new Result(false, null, null, 0, error);
    }
}
