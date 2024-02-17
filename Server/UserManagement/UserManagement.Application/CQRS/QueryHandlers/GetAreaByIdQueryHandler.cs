using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Queries;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryHandlers;

public class GetAreaByIdQueryHandler : IQueryHandler<GetAreaByIdQuery, GetAreaByIdQueryResponse>
{
    private readonly IAreaDataSource _dataSource;

    public GetAreaByIdQueryHandler(IAreaDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<GetAreaByIdQueryResponse> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
    {
        var area = await _dataSource.GetByIdAsync(request.AreaId, cancellationToken);
        if (area is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Area>(),
                ErrorMessages.NotFoundDetails((Area a) => a.Id, request.AreaId));
        }

        var response = new GetAreaByIdQueryResponse(area.AsDto());
        return response;
    }
}