using System;

namespace ModularHouse.Shared.Models.Responses;

public record ErrorResponse
{
    public ErrorInfo Error { get; private set; }
    public Guid TransactionId { get; }

    public ErrorResponse(string errorMessage, string[] errorDetails, Guid transactionId)
    {
        Error = new ErrorInfo(errorMessage, errorDetails);
        TransactionId = transactionId;
    }

    public ErrorResponse WithErrorStackTrace(string stackTrace)
    {
        Error = Error with { StackTrace = stackTrace };

        return this;
    }

    public record ErrorInfo(string Message, string[] Details)
    {
        public string StackTrace { get; init; }
    }
}