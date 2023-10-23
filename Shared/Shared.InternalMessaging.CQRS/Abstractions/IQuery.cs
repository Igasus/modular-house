using MediatR;

namespace Shared.InternalMessaging.CQRS.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}