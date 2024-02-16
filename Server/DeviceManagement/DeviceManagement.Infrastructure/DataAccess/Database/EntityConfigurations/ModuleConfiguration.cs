using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularHouse.Server.DeviceManagement.Domain.ModuleAggregate;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database.EntityConfigurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasOne(module => module.Router)
            .WithMany(router => router.Modules)
            .HasForeignKey(module => module.RouterId);

        builder.HasOne(module => module.Device)
            .WithOne(device => device.Module)
            .HasForeignKey<Module>(module => module.DeviceId);

        builder.HasOne(module => module.CreatedByUser)
            .WithMany(user => user.CreatedModules)
            .HasForeignKey(module => module.CreatedByUserId);

        builder.HasOne(module => module.LastUpdatedByUser)
            .WithMany(user => user.LastUpdatedModules)
            .HasForeignKey(module => module.LastUpdatedByUserId);
    }
}