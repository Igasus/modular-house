﻿using System;

namespace ModularHouse.Server.DeviceManagement.Application.Dto;

public record RouterDto
{
    public Guid Id { get; init; }
    public Guid AreaId { get; init; }
    public Guid DeviceId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime AdditionDate { get; init; }
}