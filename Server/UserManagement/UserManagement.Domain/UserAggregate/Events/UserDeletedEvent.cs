using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate.Events;

public record UserDeletedEvent(Guid UserId, Guid TransactionId) : IDomainEvent;