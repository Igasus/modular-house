﻿namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record ModuleInputDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}