using System;

namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record AreaDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public Guid LastUpdatedByUserId { get; set; }
}