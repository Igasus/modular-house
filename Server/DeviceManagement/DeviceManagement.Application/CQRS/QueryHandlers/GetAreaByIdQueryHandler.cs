using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetAreaByIdQueryHandler(IAreaDataSource areaDataSource)
    : IQueryHandler<GetAreaByIdQuery, GetAreaByIdQueryResponse>
{
    public async Task<GetAreaByIdQueryResponse> Handle(GetAreaByIdQuery query, CancellationToken cancellationToken)
    {
        var area = await areaDataSource.GetByIdAsync(query.AreaId, cancellationToken);
        if (area is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, query.AreaId));
        }

        return new GetAreaByIdQueryResponse(area.ToDto());
    }
}