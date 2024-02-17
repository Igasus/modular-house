using System;

namespace ModularHouse.Server.UserManagement.Application.Dto;

public record AreaDto
{
    public Guid Id { get; init; }
    public DateTime AdditionTime { get; init; }
}