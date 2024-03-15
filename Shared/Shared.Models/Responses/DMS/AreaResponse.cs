using System;

namespace ModularHouse.Shared.Models.Responses.DMS;

public record AreaResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime AdditionDate { get; init; }
}