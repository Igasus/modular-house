using MediatR;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    
}