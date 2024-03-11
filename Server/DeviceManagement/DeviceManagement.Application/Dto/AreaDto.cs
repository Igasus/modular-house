using System;

namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record AreaDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime AdditionDate { get; init; }
}