using Shared.InternalMessaging.DomainEvents.Abstractions;

namespace ModularHouse.Server.Temp.Domain.UserAggregate.Events;

public record UserCreatedEvent(Guid TransactionId, User User) : IDomainEvent;