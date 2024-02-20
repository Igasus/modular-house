using System.Threading;
using System.Threading.Tasks;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Server.UserManagement.Application.CQRS.Queries;
using ModularHouse.Server.UserManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.UserManagement.Application.MappingExtensions;
using ModularHouse.Server.UserManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.UserManagement.Application.CQRS.QueryHandlers;

public class GetModuleByIdQueryHandler : IQueryHandler<GetModuleByIdQuery, GetModuleByIdQueryResponse>
{
    private readonly IModuleDataSource _dataSource;

    public GetModuleByIdQueryHandler(IModuleDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<GetModuleByIdQueryResponse> Handle(
        GetModuleByIdQuery query,
        CancellationToken cancellationToken)
    {
        var module = await _dataSource.GetByIdAsync(query.ModuleId, cancellationToken);
        if (module is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound<Module>(),
                ErrorMessages.NotFoundDetails((Module m) => m.Id, query.ModuleId));
        }

        var response = new GetModuleByIdQueryResponse(module.AsDto());
        return response;
    }
}