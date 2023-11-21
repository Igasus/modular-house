using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

public record UserCreatedEvent(Guid UserId, Guid TransactionId) : IDomainEvent;