using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate.Events;
using ModularHouse.Server.DeviceManagement.Domain.UserAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class CreateAreaCommandHandler : ICommandHandler<CreateAreaCommand>
{
    private readonly IAreaDataSource _areaDataSource;
    private readonly IUserDataSource _userDataSource;
    private readonly IAreaRepository _areaRepository;
    private readonly IDomainEventBus _eventBus;

    public CreateAreaCommandHandler(
        IAreaDataSource areaDataSource,
        IUserDataSource userDataSource,
        IAreaRepository areaRepository,
        IDomainEventBus eventBus)
    {
        _areaDataSource = areaDataSource;
        _userDataSource = userDataSource;
        _areaRepository = areaRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(CreateAreaCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfUserIsNotExistAsync(command.Area.UserId, cancellationToken);
        await ThrowIfAreaIsNotExistAsync(command.Area.Name, cancellationToken);

        var area = new Area
        {
            Name = command.Area.Name,
            Description = command.Area.Description,
            CreationDate = DateTime.UtcNow,
            CreatedByUserId = command.Area.UserId,
            LastUpdatedDate = DateTime.UtcNow,
            LastUpdatedByUserId = command.Area.UserId
        };

        await _areaRepository.CreateAsync(area, cancellationToken);

        await _eventBus.PublishAsync(new AreaCreatedEvent(area.Id, CurrentTransaction.TransactionId));
    }

    private async Task ThrowIfUserIsNotExistAsync(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _userDataSource.GetByIdAsync(userId, cancellationToken);
        if (existingUser is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<User>(),
                ErrorMessages.NotFoundDetails((User u) => u.Id, userId));
        }
    }
    
    private async Task ThrowIfAreaIsNotExistAsync(string name, CancellationToken cancellationToken)
    {
        var existingArea = await _areaDataSource.GetByNameAsync(name, cancellationToken);
        if (existingArea is not null)
        {
            throw new BadRequestException(
                ErrorMessages.AlreadyExist<Area>(),
                ErrorMessages.AlreadyExistDetails((Area a) => a.Name, name));
        }
    }
}