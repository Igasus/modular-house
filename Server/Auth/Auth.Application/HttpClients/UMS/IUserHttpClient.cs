using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Server.Auth.Application.Dto;

namespace ModularHouse.Server.Auth.Application.HttpClients.UMS;

public interface IUserHttpClient
{
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserDto> CreateUserAsync(UserCreateDto user, CancellationToken cancellationToken = default);
}