using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;

public record GetUserQuery(Guid Id) : IQuery<User>;