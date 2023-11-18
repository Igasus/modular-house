using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

public record UserDeletedEvent(Guid TransactionId) : IDomainEvent;