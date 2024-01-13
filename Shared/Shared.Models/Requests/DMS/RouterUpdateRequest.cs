using System;

namespace ModularHouse.Shared.Models.Requests.DMS;

public record RouterUpdateRequest
{
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
};