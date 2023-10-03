namespace ModularHouse.Server.Temp.Application.Options;

public record IdentityOptions
{
    public const string Section = "Identity";
    
    public bool RequireUniqueEmail { get; set; }
    public PasswordOptions Password { get; set; }
    
    public record PasswordOptions
    {
        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
        public int RequiredUniqueChars { get; set; }
    }
}