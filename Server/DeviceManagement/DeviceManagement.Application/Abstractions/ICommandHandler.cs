using MediatR;

namespace ModularHouse.Server.DeviceManagement.Application.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}