using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetAreaByIdQueryHandler : IQueryHandler<GetAreaByIdQuery, GetAreaByIdQueryResponse>
{
    private readonly IAreaDataSource _areaDataSource;

    public GetAreaByIdQueryHandler(IAreaDataSource areaDataSource)
    {
        _areaDataSource = areaDataSource;
    }

    public async Task<GetAreaByIdQueryResponse> Handle(GetAreaByIdQuery query, CancellationToken cancellationToken)
    {
        var area = await _areaDataSource.GetByIdAsync(query.AreaId, cancellationToken);

        return new GetAreaByIdQueryResponse(area?.ToDto());
    }
}