using System;

namespace ModularHouse.Shared.Models.Requests.DMS;

public record AreaRequest(string Name, string Description, Guid UserId);