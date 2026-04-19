using CleanStore.Domain.AccountContext.Events;
using CleanStore.Domain.AccountContext.ValueObjects;
using CleanStore.Domain.SharedContext.AggregateRoots.Abstractions;
using CleanStore.Domain.SharedContext.Entities;

namespace CleanStore.Domain.AccountContext.Entities;

public sealed class Account : Entity, IAggregateRoot
{
    #region Constructors

    private Account() : base(0)
    {
    }

    private Account(
        Email email) : base(0)
    {
        Email = email;
    }

    #endregion

    #region Factories

    public static Account Create(Email email)
    {
        var account = new Account(email);
        account.RaiseDomainEvent(new OnAccountCreatedEvent(email));

        return account;
    }
    #endregion
    
    #region Properties
    public Email Email { get; private set; } = null!;
    #endregion
}