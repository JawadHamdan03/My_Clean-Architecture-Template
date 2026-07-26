using cla.Application.Common;
using cla.Application.Common.Abstractions;
using cla.Application.Features.Accounts.Reponses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cla.Application.Features.Accounts.LoginCommand;


public class LoginCommandHandler(IAppDbContext dbContext,IJwtTokenServiceProvider jwtTokenServiceProvider) : IRequestHandler<LoginCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u=>u.Name.Equals(request.Name));
        if(user is null)
        {
            throw new Exception("No User Was found");
        }

        if (!request.password.Equals(user.Password))
        {
            throw new Exception("password is not correct");
        }
       
        TokenResponse tokenResponse = await jwtTokenServiceProvider.GenerateJwtToken(user);
        return tokenResponse;
    }
}