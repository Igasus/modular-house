using MediatR;

namespace ModularHouse.Server.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}