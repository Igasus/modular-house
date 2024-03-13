using System;
using ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions.Query;

namespace ModularHouse.Server.Auth.Application.CQRS.QueryResponses;

public record GetAccessTokenQueryResponse(Guid UserId, string AccessToken) : IQueryResponse;