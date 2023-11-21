using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate.Events;

public record DeviceDeletedEvent(Guid DeviceId, Guid TransactionId) : IDomainEvent;