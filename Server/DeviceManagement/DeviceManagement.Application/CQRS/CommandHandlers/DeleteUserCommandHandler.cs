﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
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

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserQuery(command.Id), cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<User>());
        }

        _userRepository.Users.Remove(user);
        await _userRepository.Context.SaveChangesAsync(cancellationToken);
    }
}