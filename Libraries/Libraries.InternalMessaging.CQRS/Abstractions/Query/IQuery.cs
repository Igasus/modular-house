using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : IQueryResponse
{
}