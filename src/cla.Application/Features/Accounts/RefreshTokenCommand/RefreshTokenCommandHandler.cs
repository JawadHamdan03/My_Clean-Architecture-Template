using cla.Application.Common;
using cla.Application.Common.Abstractions;
using cla.Application.Features.Accounts.Reponses;
using cla.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace cla.Application.Features.Accounts.RefreshTokenCommand;

public class RefreshTokenCommandHandler(IAppDbContext dbContext,IJwtTokenServiceProvider jwtTokenServiceProvider) : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var rt = await dbContext.RefreshTokens.Include(r=>r.User).FirstOrDefaultAsync(r=> r.Token.Equals(request.refreshToken));

        if (rt is null)
            throw new Exception("No refresh token were found");

        var tokenResponse=await jwtTokenServiceProvider.GenerateJwtToken(rt.User);
        return tokenResponse;
    }
}
