using System;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;

public record UserCreatedDto(Guid Id, DateTime AdditionDate);