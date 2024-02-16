using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;

public record RouterAreaUpdatedEvent(Guid RouterId, Guid TransactionId) : IDomainEvent;