using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;

public record ModuleCreatedEvent(Guid ModuleId, Guid TransactionId) : IDomainEvent;