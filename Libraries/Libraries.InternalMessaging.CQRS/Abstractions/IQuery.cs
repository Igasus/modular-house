using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}