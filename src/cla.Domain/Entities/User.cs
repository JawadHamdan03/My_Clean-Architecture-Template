namespace cla.Domain.Entities;

public enum Role
{
    Admin,
    Customer
}
public class User
{
    public Guid Id { get; set; }=Guid.NewGuid();

    public string Name { get; set; }
    public string Password { get; set; }

    public Role Role { get; set; }
}