namespace cla.Domain.Entities;



public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}