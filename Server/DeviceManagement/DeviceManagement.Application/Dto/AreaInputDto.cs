using System;

namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record AreaInputDto(string Name, string Description, Guid UserId);