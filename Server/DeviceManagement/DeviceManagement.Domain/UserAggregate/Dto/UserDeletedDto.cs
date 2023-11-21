using System;

namespace ModularHouse.Server.DeviceManagement.Domain.UserAggregate.Dto;

public record UserDeletedDto(Guid Id, DateTime AdditionDate);