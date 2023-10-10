namespace ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;

public record DomainEventWaitResult<TEvent>(bool EventReceived, TEvent Event) where TEvent : IDomainEvent;