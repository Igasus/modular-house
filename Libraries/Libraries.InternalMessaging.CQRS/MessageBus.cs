using System.Threading.Tasks;
using MediatR;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

namespace ModularHouse.Libraries.InternalMessaging.CQRS;

public class MessageBus(ISender sender) : IMessageBus
{
    public async Task Send<TCommand>(TCommand command)
        where TCommand : ICommand =>
        await sender.Send(command);

    public async Task<TQueryResponse> Send<TQuery, TQueryResponse>(TQuery query)
        where TQuery : IQuery<TQueryResponse>
        where TQueryResponse : IQueryResponse =>
        await sender.Send(query);
}