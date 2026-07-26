using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cla.Application.Common;
using cla.Application.Common.Abstractions;
using cla.Application.Features.Accounts.Reponses;
using cla.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cla.Infrastructure.Common.Implementation;


public class jwtTokenServiceProvider(IAppDbContext dbContext,IConfiguration configuration) : IJwtTokenServiceProvider
{
    public async Task<TokenResponse> GenerateJwtToken(User request)
    {
         var jwtSettings = configuration.GetSection("JwtSettings");

        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var key = jwtSettings["SecretKey"];
        var expiry = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name.Equals(request.Name));

        

        //
        var claims = new List<Claim>()
        {
           new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
           new Claim(ClaimTypes.Role,user.Role.ToString()),
        };

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiry,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256Signature
                )
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);

        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(securityToken),
            RefreshToken="4asdas-asdasd6-asdasd13",
            ExpiresAt=expiry
        };

    }

    public Task<string> GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
}