using System;
using System.Threading.Tasks;

namespace Shared.InternalMessaging.DomainEvents.Abstractions;

public interface IDomainEventBus
{
    /// <summary>
    /// Publish Event
    /// </summary>
    /// <param name="domainEvent">DomainEvent to publish</param>
    /// <typeparam name="TEvent">Type of Event to publish</typeparam>
    /// <returns>Task</returns>
    Task PublishAsync<TEvent>(TEvent domainEvent)
        where TEvent : IDomainEvent;
    
    /// <summary>
    /// Subscribe to Event
    /// </summary>
    /// <param name="callback">Callback to invoke when specified Event is published</param>
    /// <typeparam name="TEvent">Type of Event to subscribe</typeparam>
    /// <returns>Task with Id of subscription</returns>
    Task<Guid> SubscribeAsync<TEvent>(Action<TEvent> callback)
        where TEvent : IDomainEvent;
    
    /// <summary>
    /// Unsubscribe from Event
    /// </summary>
    /// <param name="subscriptionId">Id of subscription to unsubscribe</param>
    /// <returns>Task</returns>
    Task UnsubscribeAsync(Guid subscriptionId);

    /// <summary>
    /// Wait till specified Event is published
    /// </summary>
    /// <param name="throwTimeout">
    /// (Optional) Timeout to wait. After time is up throws Exception.
    /// Default value = 10 seconds
    /// </param>
    /// <param name="transactionId">
    /// (Optional) Id of Transaction that is attached to Event.
    /// If null -> returns first published Event of specified type;
    /// Otherwise -> returns first published Event of specified type with specified attached transactionId
    /// </param>
    /// <typeparam name="TEvent">Type of Event to wait</typeparam>
    /// <returns>Task with Event</returns>
    Task<TEvent> WaitAsync<TEvent>(TimeSpan? throwTimeout = null, Guid? transactionId = null)
        where TEvent : IDomainEvent;
}