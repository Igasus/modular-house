using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetDeviceQueryHandler(IDeviceDataSource deviceDataSource)
    : IQueryHandler<GetDeviceQuery, GetDeviceQueryResponse>
{
    public async Task<GetDeviceQueryResponse> Handle(GetDeviceQuery query, CancellationToken cancellationToken)
    {
        var device = await deviceDataSource.GetByIdAsync(query.DeviceId, cancellationToken);
        var deviceDto = device.ToDto();

        return new GetDeviceQueryResponse(deviceDto);
    }
}