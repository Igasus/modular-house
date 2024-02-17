using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.UserManagement.Domain.RouterAggregate.Events;

public record RouterCreatedEvent(Guid RouterId, Guid TransactionId) : IDomainEvent;