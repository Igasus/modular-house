using System;
using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEvent : INotification
{
    Guid TransactionId { get; }
}