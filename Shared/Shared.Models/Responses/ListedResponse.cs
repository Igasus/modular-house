using System.Collections.Generic;

namespace ModularHouse.Shared.Models.Responses;

public record ListedResponse<TResponse>(IList<TResponse> List, int TotalItemsCount);