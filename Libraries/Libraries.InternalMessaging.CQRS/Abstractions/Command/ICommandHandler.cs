using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}