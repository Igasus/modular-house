using System;
using System.Threading;

namespace Common.Domain;

public class CurrentTransaction
{
    private static readonly AsyncLocal<Guid> TransactionIdAsAsyncLocal = new();

    public static Guid TransactionId
    {
        get => TransactionIdAsAsyncLocal.Value;
        private set => TransactionIdAsAsyncLocal.Value = value;
    }

    public static IDisposable SetTransactionId(Guid transactionId)
    {
        TransactionId = transactionId;
        return new DisposableAction(() => TransactionId = default);
    }
}