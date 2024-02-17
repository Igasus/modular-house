using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class CreateModuleCommandHandler : ICommandHandler<CreateModuleCommand>
{
    private readonly IModuleDataSource _dataSource;
    private readonly IModuleRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public CreateModuleCommandHandler(
        IModuleDataSource dataSource,
        IModuleRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var moduleExists = await _dataSource.ExistsByIdAsync(request.Input.Id, cancellationToken);
        if (moduleExists)
        {
            throw new BadRequestException(ErrorMessages.AlreadyExist<Module>(),
                ErrorMessages.AlreadyExistDetails((Module m) => m.Id, request.Input.Id));
        }

        var module = request.Input.AsEntity();
        await _repository.CreateAsync(module, cancellationToken);

        var moduleCreatedEvent = new ModuleCreatedEvent(module.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(moduleCreatedEvent);
    }
}