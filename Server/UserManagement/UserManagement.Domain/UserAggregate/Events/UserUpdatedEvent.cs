using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate.Events;

public record UserUpdatedEvent(Guid UserId, Guid TransactionId) : IDomainEvent;