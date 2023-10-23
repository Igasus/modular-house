using System;

namespace Shared.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEvent
{
    Guid TransactionId { get; }
}