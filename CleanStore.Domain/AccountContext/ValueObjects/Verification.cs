using CleanStore.Domain.SharedContext.ValueObjects;

namespace CleanStore.Domain.AccountContext.ValueObjects;

public sealed partial record Verification : ValueObject
{
    public string Code { get; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
    public DateTime? ExpiresAt { get;  private set; }
}