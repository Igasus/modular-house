namespace ModularHouse.Server.Temp.Domain.EventMessaging.Contracts;

public interface IDomainEvent
{
    Guid TransactionId { get; }
}