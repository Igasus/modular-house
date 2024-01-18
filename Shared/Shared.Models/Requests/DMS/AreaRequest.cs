namespace ModularHouse.Shared.Models.Requests.DMS;

public record AreaRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
}