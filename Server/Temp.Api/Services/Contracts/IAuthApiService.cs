using System;
using System.Threading.Tasks;
using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Api.Services.Contracts;

public interface IAuthApiService
{
    Task<User> SignUpAsync(Guid transactionId, string userName, string email, string password);
}