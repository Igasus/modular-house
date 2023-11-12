using System;

namespace Libraries.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEvent
{
    Guid TransactionId { get; }
}