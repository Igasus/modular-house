using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.AreaAggregate.Events;

public record AreaDeletedEvent(Guid AreaId, Guid TransactionId) : IDomainEvent;