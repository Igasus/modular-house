using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetRoutersQueryHandler : IQueryHandler<GetRoutersQuery, GetRoutersQueryResponse>
{
    private readonly IRouterDataSource _routerDataSource;

    public GetRoutersQueryHandler(IRouterDataSource routerDataSource)
    {
        _routerDataSource = routerDataSource;
    }

    public async Task<GetRoutersQueryResponse> Handle(GetRoutersQuery query, CancellationToken cancellationToken)
    {
        var routers = await _routerDataSource.GetAllAsync(cancellationToken);

        return new GetRoutersQueryResponse(routers.ToDtoList(), routers.Count);
    }
}