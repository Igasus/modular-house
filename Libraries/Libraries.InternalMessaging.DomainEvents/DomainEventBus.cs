using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModularHouse.Libraries.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Libraries.InternalMessaging.DomainEvents;

public class DomainEventBus(IPublisher publisher) : IDomainEventBus
{
    private record SubscribedEventHandler(Type EventType, Action<IDomainEvent> Callback)
    {
        public Guid SubscriptionId { get; } = Guid.NewGuid();
    }

    private readonly IList<SubscribedEventHandler> _subscribedHandlers = new List<SubscribedEventHandler>();

    public async Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        var handlersToNotify = _subscribedHandlers
            .Where(eventHandler => eventHandler.EventType == typeof(TEvent))
            .ToList();

        foreach (var eventHandler in handlersToNotify)
            eventHandler.Callback(domainEvent);

        await publisher.Publish(domainEvent);
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

        // TODO: Replace Exception with InternalServerErrorException
        if (eventHandler is null)
            throw new Exception(
                "Error while Unsubscribing from DomainEvent: Subscription with specified SubscriptionId is not found.");

        _subscribedHandlers.Remove(eventHandler);

        return Task.CompletedTask;
    }

    public async Task<TEvent> WaitAsync<TEvent>(TimeSpan? throwTimeout = null, Guid? transactionId = null)
        where TEvent : IDomainEvent
    {
        throwTimeout ??= Constraints.DefaultEventWaitTimeout;

        var domainEvent = default(TEvent);
        var cancellationTokenSource = new CancellationTokenSource();

        var subscriptionId = await SubscribeAsync((Action<TEvent>)WaitingEventHandler);

        try
        {
            await Task.Delay(throwTimeout.Value, cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            // ignored
        }

        await UnsubscribeAsync(subscriptionId);

        if (cancellationTokenSource.IsCancellationRequested)
            return domainEvent;

        var exceptionMessage = $"Time out while waiting Event {typeof(TEvent).Name} with " + (transactionId is null
            ? "any TransactionId."
            : $"TransactionId = {transactionId.Value}");

        throw new Exception(exceptionMessage);

        void WaitingEventHandler(TEvent value)
        {
            if (transactionId is not null && value.TransactionId != transactionId)
                return;

            domainEvent = value;
            cancellationTokenSource.Cancel();
        }
    }
}