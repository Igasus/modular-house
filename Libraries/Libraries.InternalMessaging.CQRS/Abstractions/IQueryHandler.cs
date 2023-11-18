﻿using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.CQRS.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}