using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Events;

public record UserDeletedEvent(UserDeletedDto User, Guid TransactionId) : IDomainEvent;