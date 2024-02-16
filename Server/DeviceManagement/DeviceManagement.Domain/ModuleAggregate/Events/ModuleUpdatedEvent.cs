﻿using System;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate.Events;

public record ModuleUpdatedEvent(Guid ModuleId, Guid TransactionId) : IDomainEvent;