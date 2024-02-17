using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Command;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Commands;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate.Events;

namespace ModularHouse.Server.UserManagement.Application.CQRS.CommandHandlers;

public class DeleteModuleByIdCommandHandler : ICommandHandler<DeleteModuleByIdCommand>
{
    private readonly IModuleDataSource _dataSource;
    private readonly IModuleRepository _repository;
    private readonly IDomainEventBus _domainEventBus;

    public DeleteModuleByIdCommandHandler(
        IModuleDataSource dataSource,
        IModuleRepository repository,
        IDomainEventBus domainEventBus)
    {
        _dataSource = dataSource;
        _repository = repository;
        _domainEventBus = domainEventBus;
    }

    public async Task Handle(DeleteModuleByIdCommand request, CancellationToken cancellationToken)
    {
        var module = await _dataSource.GetByIdAsync(request.ModuleId, cancellationToken);
        if (module is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Module>(),
                ErrorMessages.NotFoundDetails((Module a) => a.Id, request.ModuleId));
        }

        await _repository.DeleteAsync(module, cancellationToken);

        var moduleDeletedEvent = new ModuleDeletedEvent(module.Id, CurrentTransaction.TransactionId);
        await _domainEventBus.PublishAsync(moduleDeletedEvent);
    }
}