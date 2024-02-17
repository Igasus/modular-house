using System;

namespace ModularHouse.Shared.Models.Responses.UMS;

public record RouterResponse
{
    public Guid Id { get; init; }
    public DateTime AdditionDate { get; init; }
}