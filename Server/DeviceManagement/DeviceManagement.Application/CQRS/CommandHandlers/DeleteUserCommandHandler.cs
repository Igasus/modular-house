using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserQuery(request.Id), cancellationToken);

        _userRepository.Users.Remove(user);
        await _userRepository.Context.SaveChangesAsync(cancellationToken);
    }
}