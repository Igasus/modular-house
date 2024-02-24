﻿using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.DeviceManagement.Application.CQRS.Queries;
using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.DataMappers;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.CQRS.QueryHandlers;

public class GetRouterByIdQueryHandler(IRouterDataSource routerDataSource)
    : IQueryHandler<GetRouterByIdQuery, GetRouterByIdQueryResponse>
{
    public async Task<GetRouterByIdQueryResponse> Handle(GetRouterByIdQuery query, CancellationToken cancellationToken)
    {
        var router = await routerDataSource.GetByIdAsync(query.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(
                ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router r) => r.Id, query.RouterId));
        }

        return new GetRouterByIdQueryResponse(router.ToDto());
    }
}