using System;
using System.Linq;
using System.Security.Cryptography;
using ModularHouse.Server.Common.Domain.Exceptions;

namespace ModularHouse.Server.Auth.Domain.UserAggregate;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime AdditionDate { get; set; }

    private static class PasswordHashingConfiguration
    {
        public const char PasswordSaltHashDivider = '-';
        public const int MaxSaltBytesCount = 16;
        public const int HashingIterationsCount = 8192;
        public const int HashingKeyBytesNumber = 256;
    }

    public void SetPassword(string password)
    {
        var bytesCount = Random.Shared.Next() % PasswordHashingConfiguration.MaxSaltBytesCount + 1;
        var salt = RandomNumberGenerator.GetBytes(bytesCount);
        var saltAsString = Convert.ToBase64String(salt);

        PasswordHash = CreatePasswordHash(password, salt, saltAsString);
    }

    public bool ValidatePassword(string password)
    {
        if (PasswordHash == default)
        {
            throw new InternalServerException("Error while validating password.",
                "Unable to validate password: PasswordHash is not set.");
        }

        var splitPasswordHash = PasswordHash.Split(PasswordHashingConfiguration.PasswordSaltHashDivider);
        if (splitPasswordHash.Length <= 1)
        {
            throw new InternalServerException("Error while validating password.",
                "Unable to validate password: invalid PasswordHash.");
        }

        var saltAsString = splitPasswordHash.First();
        var salt = Convert.FromBase64String(saltAsString);
        var passwordHash = CreatePasswordHash(password, salt, saltAsString);

        return PasswordHash == passwordHash;
    }

    private string CreatePasswordHash(string password, byte[] salt, string saltAsString)
    {
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(
            $"{saltAsString}{PasswordHashingConfiguration.PasswordSaltHashDivider}{password}",
            salt,
            PasswordHashingConfiguration.HashingIterationsCount,
            HashAlgorithmName.SHA256);
        var hash = rfc2898DeriveBytes.GetBytes(PasswordHashingConfiguration.HashingKeyBytesNumber);
        var hashAsString = Convert.ToBase64String(hash);

        return $"{saltAsString}-{hashAsString}";
    }
}