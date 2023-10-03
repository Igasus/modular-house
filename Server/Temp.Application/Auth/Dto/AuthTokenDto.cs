using System;

namespace ModularHouse.Server.Application.Auth.Dto;

public record AuthTokenDto(string Value, DateTimeOffset ExpirationDate);