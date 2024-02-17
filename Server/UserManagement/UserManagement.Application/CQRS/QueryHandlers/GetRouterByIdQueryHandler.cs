using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Queries;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryHandlers;

public class GetRouterByIdQueryHandler : IQueryHandler<GetRouterByIdQuery, GetRouterByIdQueryResponse>
{
    private readonly IRouterDataSource _dataSource;

    public GetRouterByIdQueryHandler(IRouterDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<GetRouterByIdQueryResponse> Handle(
        GetRouterByIdQuery request,
        CancellationToken cancellationToken)
    {
        var router = await _dataSource.GetByIdAsync(request.RouterId, cancellationToken);
        if (router is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Router>(),
                ErrorMessages.NotFoundDetails((Router r) => r.Id, request.RouterId));
        }

        var response = new GetRouterByIdQueryResponse(router.AsDto());
        return response;
    }
}