using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetModulesQueryHandler : IQueryHandler<GetModulesQuery, GetModulesQueryResponse>
{
    private readonly IModuleDataSource _moduleDataSource;

    public GetModulesQueryHandler(IModuleDataSource moduleDataSource)
    {
        _moduleDataSource = moduleDataSource;
    }

    public async Task<GetModulesQueryResponse> Handle(GetModulesQuery query, CancellationToken cancellationToken)
    {
        var modules = await _moduleDataSource.GetAllAsync(cancellationToken);

        return new GetModulesQueryResponse(modules.ToDtoList(), modules.Count);
    }
}