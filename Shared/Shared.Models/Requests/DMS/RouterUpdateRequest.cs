namespace ModularHouse.Shared.Models.Requests.DMS;

public record RouterUpdateRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
};