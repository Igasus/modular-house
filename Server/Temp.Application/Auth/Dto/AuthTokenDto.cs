using System;

namespace ModularHouse.Server.Temp.Application.Auth.Dto;

public record AuthTokenDto(string Value, DateTimeOffset ExpirationDate);