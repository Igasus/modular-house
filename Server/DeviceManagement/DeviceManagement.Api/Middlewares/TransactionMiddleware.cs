using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ModularHouse.Server.DeviceManagement.Domain.Common;

namespace ModularHouse.Server.DeviceManagement.Api.Middlewares;

public class TransactionMiddleware : IMiddleware
{
    private const string TRANSACTION_ID_HEADER = "TransactionId";
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var transactionId = context.Request.Headers[TRANSACTION_ID_HEADER].ToString();
        if (string.IsNullOrEmpty(transactionId))
        {
            transactionId = Guid.NewGuid().ToString();
            context.Request.Headers[TRANSACTION_ID_HEADER] = transactionId;
        }

        using (CurrentTransaction.SetTransactionId(transactionId))
        {
            await next(context);
        }
    }
}