using CleanStore.Domain.AccountContext.ValueObjects;
using FluentValidation;

namespace CleanStore.Application.AccountContext.UseCases.Create;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required");
    }
}