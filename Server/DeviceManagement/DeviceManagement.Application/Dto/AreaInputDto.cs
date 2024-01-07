using System;

namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record AreaInputDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid UserId { get; init; }
}