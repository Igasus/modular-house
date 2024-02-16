using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;

public record RouterUpdatedEvent(Guid RouterId, Guid TransactionId) : IDomainEvent;