using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace cla.Application.Features.Accounts.RefreshTokenCommand;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.refreshToken).NotEmpty().WithMessage("Refresh token can't be empty");
    }
}
