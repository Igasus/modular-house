using System;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEvent
{
    Guid TransactionId { get; }
}