using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

public record UserCreatedEvent(UserCreatedDto User, Guid TransactionId) : IDomainEvent;