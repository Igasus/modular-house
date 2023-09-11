namespace ModularHouse.Server.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}