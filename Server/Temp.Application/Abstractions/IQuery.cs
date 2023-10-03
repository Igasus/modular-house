using MediatR;

namespace ModularHouse.Server.Temp.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}