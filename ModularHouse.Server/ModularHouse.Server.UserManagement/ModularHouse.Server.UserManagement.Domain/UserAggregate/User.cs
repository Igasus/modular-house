namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; private set; }

    public void SetPassword()
    {
        throw new NotImplementedException();
    }

    public bool ValidatePassword()
    {
        throw new NotImplementedException();
    }
}