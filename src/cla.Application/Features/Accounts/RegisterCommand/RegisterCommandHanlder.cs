using cla.Application.Common;
using cla.Application.Common.Abstractions;
using cla.Application.Features.Accounts.Reponses;
using cla.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cla.Application.Features.Accounts.RegisterCommand;


public class RegisterCommandHandler(IAppDbContext dbContext, IJwtTokenServiceProvider jwtTokenServiceProvider)
:IRequestHandler<RegisterCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u=>u.Name.Equals(request.Name));

        if(user is not null)
        {
            throw new Exception($"User {request.Name} Already Exists");
        }

        var newUser = new User
        {
          Name=request.Name,
          Password=request.Password,
          Role=Role.Customer  
        };

        await dbContext.Users.AddAsync(newUser);
        await dbContext.SaveChangesAsync(cancellationToken);

        var res= await jwtTokenServiceProvider.GenerateJwtToken(newUser);
        return res;
    }
}