using cla.Application.Features.Accounts.Reponses;
using cla.Domain.Entities;

namespace cla.Application.Common.Abstractions;


public interface IJwtTokenServiceProvider
{
    Task<TokenResponse> GenerateJwtToken(User user);
    Task<string> GenerateRefreshToken();
}