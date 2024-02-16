﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Commands;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate.Events;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.CommandHandlers;

public class UpdateRouterAreaByIdCommandHandler : ICommandHandler<UpdateRouterAreaByIdCommand>
{
    private readonly IRouterDataSource _routerDataSource;
    private readonly IRouterRepository _routerRepository;
    private readonly IAreaDataSource _areaDataSource;
    private readonly IDomainEventBus _eventBus;

    public UpdateRouterAreaByIdCommandHandler(
        IRouterDataSource routerDataSource,
        IRouterRepository routerRepository,
        IAreaDataSource areaDataSource,
        IDomainEventBus eventBus)
    {
        _routerDataSource = routerDataSource;
        _routerRepository = routerRepository;
        _areaDataSource = areaDataSource;
        _eventBus = eventBus;
    }

    public async Task Handle(UpdateRouterAreaByIdCommand command, CancellationToken cancellationToken)
    {
        await ThrowIfAreaIsNotExistByIdAsync(command.AreaId, cancellationToken);
        
        var router = await _routerDataSource.GetByIdAsync(command.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router r) => r.Id, command.RouterId));
        }

        router.AreaId = command.AreaId;
        await _routerRepository.UpdateAsync(router, cancellationToken);

        await _eventBus.PublishAsync(new RouterAreaUpdatedEvent(router.Id, CurrentTransaction.TransactionId));
    }

    private async Task ThrowIfAreaIsNotExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var isAreaExist = await _areaDataSource.ExistByIdAsync(id, cancellationToken);
        if (!isAreaExist)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, id));
        }
    }
}