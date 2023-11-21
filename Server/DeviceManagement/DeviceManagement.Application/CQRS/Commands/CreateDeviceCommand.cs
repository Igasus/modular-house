﻿using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;

public record CreateDeviceCommand(Guid DeviceId) : ICommand;