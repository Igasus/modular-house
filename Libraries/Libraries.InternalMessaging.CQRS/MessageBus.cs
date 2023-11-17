using System.Threading.Tasks;
using MediatR;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

namespace ModularHouse.Libraries.InternalMessaging.CQRS;

public class MessageBus : IMessageBus
{
    private readonly ISender _sender;

    public MessageBus(ISender sender)
    {
        _sender = sender;
    }

    public async Task Send<TCommand>(TCommand command)
        where TCommand : ICommand =>
        await _sender.Send(command);

    public async Task<TQueryResponse> Send<TQuery, TQueryResponse>(TQuery query)
        where TQuery : IQuery<TQueryResponse>
        where TQueryResponse : IQueryResponse =>
        await _sender.Send(query);
}