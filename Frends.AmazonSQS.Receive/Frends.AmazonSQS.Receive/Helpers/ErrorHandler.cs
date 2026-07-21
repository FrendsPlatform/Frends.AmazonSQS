using Frends.AmazonSQS.Receive.Definitions;
using System;

namespace Frends.AmazonSQS.Receive.Helpers;

internal static class ErrorHandler
{
    internal static Result Handle(Exception exception, Options options)
    {
        if (exception is OperationCanceledException) throw exception;

        if (options.ThrowErrorOnFailure)
        {
            if (string.IsNullOrEmpty(options.ErrorMessageOnFailure))
                throw new Exception(exception.Message, exception);

            throw new Exception(options.ErrorMessageOnFailure, exception);
        }

        var errorMessage = !string.IsNullOrEmpty(options.ErrorMessageOnFailure)
            ? $"{options.ErrorMessageOnFailure}: {exception.Message}"
            : exception.Message;

        return new Result(new Error
        {
            Message = errorMessage,
            AdditionalInfo = exception,
        });
    }
}
