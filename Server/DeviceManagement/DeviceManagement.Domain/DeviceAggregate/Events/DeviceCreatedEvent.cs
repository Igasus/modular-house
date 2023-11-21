using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate.Events;

public record DeviceCreatedEvent(Guid DeviceId, Guid TransactionId) : IDomainEvent;