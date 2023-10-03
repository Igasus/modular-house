using MediatR;

namespace ModularHouse.Server.Temp.Application.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}