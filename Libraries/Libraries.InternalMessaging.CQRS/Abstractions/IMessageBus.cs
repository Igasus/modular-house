using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

namespace ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;

public interface IMessageBus
{
    Task Send<TCommand>(TCommand command)
        where TCommand : ICommand;

    Task<TQueryResponse> Send<TQuery, TQueryResponse>(TQuery query)
        where TQuery : IQuery<TQueryResponse>
        where TQueryResponse : IQueryResponse;
}