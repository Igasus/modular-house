namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record RouterInputDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}