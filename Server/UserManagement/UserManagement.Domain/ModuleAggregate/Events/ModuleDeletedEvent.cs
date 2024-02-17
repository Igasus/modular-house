using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;

public record ModuleDeletedEvent(Guid ModuleId, Guid TransactionId) : IDomainEvent;