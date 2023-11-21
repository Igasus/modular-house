using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetDeviceQueryHandler : IQueryHandler<GetDeviceQuery, GetDeviceQueryResponse>
{
    private readonly IDeviceDataSource _deviceDataSource;

    public GetDeviceQueryHandler(IDeviceDataSource deviceDataSource)
    {
        _deviceDataSource = deviceDataSource;
    }

    public async Task<GetDeviceQueryResponse> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
    {
        var device = await _deviceDataSource.GetByIdAsync(request.DeviceId, cancellationToken);
        return device.ToResponse();
    }
}