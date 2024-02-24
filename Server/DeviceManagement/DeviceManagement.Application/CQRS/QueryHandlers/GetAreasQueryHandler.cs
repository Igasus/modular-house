using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetAreasQueryHandler(IAreaDataSource areaDataSource) : IQueryHandler<GetAreasQuery, GetAreasQueryResponse>
{
    public async Task<GetAreasQueryResponse> Handle(GetAreasQuery query, CancellationToken cancellationToken)
    {
        var areas = await areaDataSource.GetAllAsync(cancellationToken);

        return new GetAreasQueryResponse(areas.ToDtoList(), areas.Count);
    }
}