using System;

namespace ModularHouse.Shared.Models.Requests.DMS;

public record AreaRequest
{
    public string Name { get; }
    public string Description { get; }
    public Guid UserId { get; }
}