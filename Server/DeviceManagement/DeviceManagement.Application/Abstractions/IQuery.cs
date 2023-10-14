using MediatR;

namespace ModularHouse.Server.DeviceManagement.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}