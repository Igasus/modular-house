using ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;

namespace ModularHouse.Server.Temp.Domain.UserAggregate.Events;

public record UserCreatedEvent(Guid TransactionId, User User) : IDomainEvent;