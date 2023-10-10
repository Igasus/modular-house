namespace ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;

public interface IDomainEventBus
{
    Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    Task<Guid> SubscribeAsync<TEvent>(Action<TEvent> callback) where TEvent : IDomainEvent;
    Task UnsubscribeAsync(Guid subscriptionId);
    
    Task<DomainEventWaitResult<TEvent>> WaitAsync<TEvent>(int milliseconds, Guid? transactionId = null)
        where TEvent : IDomainEvent;
}