using System;

namespace ModularHouse.Server.UserManagement.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public void SetPassword(string password)
    {
        throw new NotImplementedException();
    }

    public bool ValidatePassword(string password)
    {
        throw new NotImplementedException();
    }
}