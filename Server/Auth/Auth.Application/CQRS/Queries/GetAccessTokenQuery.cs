using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;
using ModularHouse.Server.Auth.Application.CQRS.QueryResponses;
using ModularHouse.Server.Auth.Application.Dto;

namespace ModularHouse.Server.Auth.Application.CQRS.Queries;

public record GetAccessTokenQuery(UserCredentials Credentials) : IQuery<GetAccessTokenQueryResponse>;