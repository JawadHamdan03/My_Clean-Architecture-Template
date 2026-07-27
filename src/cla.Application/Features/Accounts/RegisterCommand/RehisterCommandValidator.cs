using System.Data;
using FluentValidation;

namespace cla.Application.Features.Accounts.RegisterCommand;


public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name can't be empty");
        RuleFor(x=>x.Password).NotEmpty().WithMessage("Password can't be empty");
    }
}