using MediatR;

namespace Libraries.InternalMessaging.CQRS.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}