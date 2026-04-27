using System.Text.RegularExpressions;
using CleanStore.Domain.AccountContext.Exceptions;
using CleanStore.Domain.SharedContext.Extensions;
using CleanStore.Domain.SharedContext.ValueObjects;

namespace CleanStore.Domain.AccountContext.ValueObjects;

public sealed partial record Email : ValueObject
{
    #region Constants
    // Regras estáticas e metadados usados na validação/formatação do e-mail.
    public const int MinLength = 6;
    public const int MaxLength = 160;
    public const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    #endregion

    #region Constructors
    // Construtores privados para forçar a criação controlada via fábrica.
    private Email()
    {
    }

    private Email(string address, string hash, Verification verification)
    {
        Address = address;
        Hash = hash;
        Verification = verification;
    }
    #endregion

    #region Factories
    // Ponto único de criação com saneamento e validações de domínio.
    public static Email Create(string address)
    {
        if (string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(address))
            throw new EmailNullOrEmptyException(ErrorMessages.Email.NullOrEmpty);
        
        address = address.Trim();
        address = address.ToLower();
        
        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException(ErrorMessages.Email.Invalid);

        return new Email(address, address.ToBase64(), Verification.Create());
    }
    #endregion

    #region Properties
    // Estado interno do Value Object e dependências de verificação associadas.
    public string Address { get; private set; } = string.Empty;
    public string Hash { get; private set; } = string.Empty;
    public Verification Verification { get; private set; } = null!;
    #endregion
    
    #region Operators
    // Conversão implícita para facilitar uso em contextos que esperam string.
    public static implicit operator string(Email email) => email.ToString();
    #endregion

    #region Overrides
    // Representação textual canônica do Value Object.
    public override string ToString() => Address;
    #endregion

    #region Other
    // Regex compilada em tempo de build para validação com melhor performance.
    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
    #endregion
}