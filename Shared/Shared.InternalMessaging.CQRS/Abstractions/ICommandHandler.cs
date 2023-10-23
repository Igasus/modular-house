using MediatR;

namespace Shared.InternalMessaging.CQRS.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}