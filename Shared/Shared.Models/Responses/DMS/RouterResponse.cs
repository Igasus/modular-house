using System;

namespace ModularHouse.Shared.Models.Responses.DMS;

public record RouterResponse
{
    public Guid Id { get; init; }
    public Guid AreaId { get; init; }
    public Guid DeviceId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime AdditionDate { get; init; }
}