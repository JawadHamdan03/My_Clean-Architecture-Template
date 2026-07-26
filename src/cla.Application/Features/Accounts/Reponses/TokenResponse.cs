namespace cla.Application.Features.Accounts.Reponses;


public class TokenResponse
{
    public string? AccessToken{get;set;}
    public string? RefreshToken{get;set;}
    public DateTime ExpiresAt{get;set;}

}