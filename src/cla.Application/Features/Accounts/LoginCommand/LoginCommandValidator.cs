using FluentValidation;

namespace cla.Application.Features.Accounts.LoginCommand;


public class LoginCommandValidator:AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name can't be empty");
        RuleFor(x=>x.password).NotEmpty().WithMessage("password can't be empty");

    }
}