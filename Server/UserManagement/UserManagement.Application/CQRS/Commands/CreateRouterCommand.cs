﻿using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Server.UserManagement.Application.Dto;

namespace ModularHouse.Server.UserManagement.Application.CQRS.Commands;

public record CreateRouterCommand(RouterDtoInput Input) : ICommand;