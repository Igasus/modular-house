using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.Auth.Domain.UserAggregate.Events;

public record UserCreatedEvent(Guid UserId, Guid TransactionId) : IDomainEvent;