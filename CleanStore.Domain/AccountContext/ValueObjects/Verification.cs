using CleanStore.Domain.SharedContext.Exceptions;
using CleanStore.Domain.SharedContext.ValueObjects;

namespace CleanStore.Domain.AccountContext.ValueObjects;

public sealed partial record Verification : ValueObject
{
    #region Constants
    // Metadados estáticos usados para geração e expiração do código.
    private const int CodeLength = 6;
    private const int ExpirationInHours = 5;
    #endregion

    #region Constructors
    // Construtores privados para forçar a criação controlada via fábrica.
    private Verification()
    {
    }

    private Verification(string code, DateTime? expiresAt, DateTime? verifiedAt)
    {
        Code = code;
        ExpiresAt = expiresAt;
        VerifiedAt = verifiedAt;
    }
    #endregion

    #region Factories
    // Ponto único de criação para garantir formato e janela de validade do código.
    public static Verification Create()
    {
        var code = Guid.NewGuid().ToString("N")[0..CodeLength].ToUpperInvariant();
        var expiresAt = DateTime.UtcNow.AddHours(ExpirationInHours);

        return new Verification(code, expiresAt, null);
    }
    #endregion

    #region Properties
    // Estado interno do Value Object para fluxo de validação e ativação.
    public string Code { get; private set; } = string.Empty;
    public DateTime? ExpiresAt { get; private set; } = null;
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;
    #endregion

    #region Operators
    // Conversão implícita para facilitar uso em contextos que esperam string.
    public static implicit operator string(Verification verification) => verification.ToString();
    #endregion

    #region Overrides
    // Representação textual canônica do Value Object.
    public override string ToString() => Code;
    #endregion

    #region Other
    // Confirma o código recebido e ativa a verificação quando válido e não expirada.
    public void Verify(string code)
    {
        if (IsActive)
            throw new DomainException("Verification is already active.");

        if (ExpiresAt < DateTime.UtcNow)
            throw new DomainException("Verification has expired.");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new DomainException("Código de verificação inválido");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
    #endregion
}