using CleanStore.Domain.SharedContext.Events.Abstractions;

namespace CleanStore.Domain.AccountContext.Events;

public sealed record OnAccountCreatedEvent(int Id, string Email) : IDomainEvent;