using MediatR;

namespace Libraries.InternalMessaging.CQRS.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}