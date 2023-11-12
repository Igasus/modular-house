using System;
using System.Threading;

namespace ModularHouse.Server.DeviceManagement.Domain.Common;

public class CurrentTransaction
{
    private static readonly AsyncLocal<string> _transactionId = new();

    public static string TransactionId
    {
        get => _transactionId.Value;
        private set => _transactionId.Value = value;
    }

    public static IDisposable SetTransactionId(string transactionId)
    {
        TransactionId = transactionId;
        return new DisposableAction(() => TransactionId = string.Empty);
    }
}