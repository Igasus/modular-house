using System;

namespace ModularHouse.Shared.Models.Requests.DMS;

public record RouterCreateRequest
{
    public Guid AreaId { get; init; }
    public Guid DeviceId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
};