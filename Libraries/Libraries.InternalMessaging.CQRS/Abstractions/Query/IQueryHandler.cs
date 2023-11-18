using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : IQueryResponse
{
}