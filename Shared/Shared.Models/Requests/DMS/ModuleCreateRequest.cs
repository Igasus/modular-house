using System;

namespace ModularHouse.Shared.Models.Requests.DMS;

public record ModuleCreateRequest
{
    public Guid RouterId { get; init; }
    public Guid DeviceId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}