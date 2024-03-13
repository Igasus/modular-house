using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularHouse.Server.DeviceManagement.Domain.RouterAggregate;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database.EntityConfigurations;

public class RouterConfiguration : IEntityTypeConfiguration<Router>
{
    public void Configure(EntityTypeBuilder<Router> builder)
    {
        builder.HasOne(router => router.Area)
            .WithMany(area => area.Routers)
            .HasForeignKey(router => router.AreaId);

        builder.HasOne(router => router.Device)
            .WithOne(device => device.Router)
            .HasForeignKey<Router>(x => x.DeviceId);
    }
}