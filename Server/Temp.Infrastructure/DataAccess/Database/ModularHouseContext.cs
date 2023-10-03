using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModularHouse.Server.Temp.Domain.UserAggregate;

namespace ModularHouse.Server.Temp.Infrastructure.DataAccess.Database;

public class ModularHouseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ModularHouseContext(DbContextOptions<ModularHouseContext> options) : base(options)
    {
    }
}