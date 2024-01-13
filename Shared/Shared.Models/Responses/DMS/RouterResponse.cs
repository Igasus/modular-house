using System;

namespace ModularHouse.Shared.Models.Responses.DMS;

public record RouterResponse
{
    public Guid Id { get; init; }
    public Guid AreaId { get; init; }
    public Guid DeviceId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime CreationDate { get; init; }
    public Guid CreatedByUserId { get; init; }
    public DateTime LastUpdatedDate { get; init; }
    public Guid LastUpdatedByUserId { get; init; }
}