using System;

namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record ModuleDto
{
    public Guid Id { get; init; }
    public Guid RouterId { get; init; }
    public Guid DeviceId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime AdditionDate { get; init; }
}