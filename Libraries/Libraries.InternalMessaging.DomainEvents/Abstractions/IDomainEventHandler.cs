using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}