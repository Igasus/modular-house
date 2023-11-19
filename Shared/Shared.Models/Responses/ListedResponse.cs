using System.Collections.Generic;

namespace ModularHouse.Shared.Models.Responses;

public record ListedResponse<TResponse>(IReadOnlyList<TResponse> List, int TotalItemsCount);