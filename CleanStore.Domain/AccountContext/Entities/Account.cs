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
        int id,
        Email email) : base(id)
    {

        Email = email;
    }

    #endregion

    #region Factories

    public static Account Create(int id, Email email)
    {
        var account = new Account(id, email);
        account.RaiseDomainEvent(new OnAccountCreatedEvent(id, email));

        return account;
    }
    
    #endregion
    
    #region Properties
    public Email Email { get; private set; } = null!;
    #endregion
}