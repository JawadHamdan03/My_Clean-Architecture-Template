using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        var refToken= await GenerateRefreshToken(request);
        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(securityToken),
            RefreshToken=refToken,
            ExpiresAt=expiry
        };

    }

    public async Task<string> GenerateRefreshToken(User user)
    {
        var dbUser = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.Name.Equals(user.Name));

        if (dbUser is null)
        {
            throw new Exception("User Was Not Found");
        }

        var userRefreshToken = await dbContext.RefreshTokens.Where(r=>r.UserId.Equals(dbUser.Id)).ExecuteDeleteAsync();

        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        RefreshToken refToken = new RefreshToken
        {
            Token=rawToken,
            UserId=dbUser.Id,
            ExpiresAt=DateTime.UtcNow.AddDays(7)
        };

        await dbContext.RefreshTokens.AddAsync(refToken);
        await dbContext.SaveChangesAsync(new CancellationToken());

        return rawToken;

    }
}