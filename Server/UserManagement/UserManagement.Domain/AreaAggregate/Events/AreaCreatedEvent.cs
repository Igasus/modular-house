using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.UserManagement.Domain.AreaAggregate.Events;

public record AreaCreatedEvent(Guid AreaId, Guid TransactionId) : IDomainEvent;