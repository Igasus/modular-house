using System;

namespace ModularHouse.Server.DeviceManagement.Api.Http.Controllers.Users.Responses;

public record CreatedUserResponse(Guid Id, DateTime AdditionDate);