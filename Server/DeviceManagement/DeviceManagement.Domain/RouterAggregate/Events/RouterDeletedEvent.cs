using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;

public record RouterDeletedEvent(Guid RouterId, Guid TransactionId) : IDomainEvent;