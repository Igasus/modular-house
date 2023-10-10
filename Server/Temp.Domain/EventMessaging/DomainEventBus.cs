using ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;
using ModularHouse.Server.Temp.Domain.Exceptions;

namespace ModularHouse.Server.Temp.Domain.EventMessaging;

public class DomainEventBus : IDomainEventBus
{
    private record SubscribedEventHandler(Type EventType, Action<IDomainEvent> Callback)
    {
        public Guid SubscriptionId { get; } = Guid.NewGuid();
    }

    private readonly IList<SubscribedEventHandler> _subscribedHandlers = new List<SubscribedEventHandler>();
    
    public Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        var handlersToNotify = _subscribedHandlers
            .Where(eventHandler => eventHandler.EventType == typeof(TEvent))
            .ToList();

        foreach (var eventHandler in handlersToNotify)
            eventHandler.Callback(domainEvent);

        return Task.CompletedTask;
    }

    public Task<Guid> SubscribeAsync<TEvent>(Action<TEvent> callback) where TEvent : IDomainEvent
    {
        var eventHandler = new SubscribedEventHandler(typeof(TEvent), AbstractedCallback);
        _subscribedHandlers.Add(eventHandler);

        return Task.FromResult(eventHandler.SubscriptionId);
        
        void AbstractedCallback(IDomainEvent evt) => callback((TEvent)evt);
    }

    public Task UnsubscribeAsync(Guid subscriptionId)
    {
        var eventHandler = _subscribedHandlers
            .FirstOrDefault(handler => handler.SubscriptionId == subscriptionId);

        if (eventHandler is null)
            throw new InternalServerErrorException(
                "Error while Unsubscribing from DomainEvent: Subscription with specified SubscriptionId is not found.");

        _subscribedHandlers.Remove(eventHandler);

        return Task.CompletedTask;
    }

    public async Task<DomainEventWaitResult<TEvent>> WaitAsync<TEvent>(int milliseconds, Guid? transactionId = null)
        where TEvent : IDomainEvent
    {
        var domainEvent = default(TEvent);
        var cancellationTokenSource = new CancellationTokenSource();

        var subscriptionId = await SubscribeAsync((Action<TEvent>)WaitingEventHandler);

        try
        {
            await Task.Delay(milliseconds, cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            // ignored
        }

        await UnsubscribeAsync(subscriptionId);

        var eventReceived = cancellationTokenSource.IsCancellationRequested;
        var result = new DomainEventWaitResult<TEvent>(eventReceived, domainEvent);
        
        return result;
        
        void WaitingEventHandler(TEvent value)
        {
            if (transactionId is not null && value.TransactionId != transactionId)
                return;

            domainEvent = value;
            cancellationTokenSource.Cancel();
        }
    }
}