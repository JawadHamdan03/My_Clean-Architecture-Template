using cla.Application.Features.Accounts.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace cla.Application.Features.Accounts.RefreshTokenCommand;

public sealed record RefreshTokenCommand(string refreshToken) : IRequest<TokenResponse>;
