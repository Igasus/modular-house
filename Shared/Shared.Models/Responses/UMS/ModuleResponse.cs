using System;

namespace ModularHouse.Shared.Models.Responses.UMS;

public record ModuleResponse
{
    public Guid Id { get; init; }
    public DateTime AdditionDate { get; init; }
}