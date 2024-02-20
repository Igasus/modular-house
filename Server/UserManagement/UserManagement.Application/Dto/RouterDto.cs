using System;

namespace ModularHouse.Server.UserManagement.Application.Dto;

public record RouterDto
{
    public Guid Id { get; init; }
    public DateTime AdditionTime { get; init; }
}