using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularHouse.Server.DeviceManagement.Domain.AreaAggregate;

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database.EntityConfigurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.HasOne(area => area.CreatedByUser)
            .WithMany(user => user.CreatedAreas)
            .HasForeignKey(area => area.CreatedByUserId);
        
        builder.HasOne(area => area.LastUpdatedByUser)
            .WithMany(user => user.LastUpdatedAreas)
            .HasForeignKey(area => area.LastUpdatedByUserId);
    }
}