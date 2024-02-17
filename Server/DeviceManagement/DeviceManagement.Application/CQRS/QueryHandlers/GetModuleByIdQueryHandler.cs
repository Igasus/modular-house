using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetModuleByIdQueryHandler : IQueryHandler<GetModuleByIdQuery, GetModuleByIdQueryResponse>
{
    private readonly IModuleDataSource _moduleDataSource;

    public GetModuleByIdQueryHandler(IModuleDataSource moduleDataSource)
    {
        _moduleDataSource = moduleDataSource;
    }

    public async Task<GetModuleByIdQueryResponse> Handle(
        GetModuleByIdQuery query,
        CancellationToken cancellationToken)
    {
        var module = await _moduleDataSource.GetByIdAsync(query.ModuleId, cancellationToken);
        if (module is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Module>(),
                ErrorMessages.NotFoundDetails((Module m) => m.Id, query.ModuleId));
        }

        return new GetModuleByIdQueryResponse(module.ToDto());
    }
}