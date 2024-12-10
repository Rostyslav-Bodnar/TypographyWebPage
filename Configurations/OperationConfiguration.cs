using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Configurations
{
    public class OperationConfiguration : IEntityTypeConfiguration<OperationEntity>
    {
        public void Configure(EntityTypeBuilder<OperationEntity> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .HasMany(o => o.RoleOperations)
                .WithOne(o => o.Operation);
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasMany(o => o.RoleOperations)
                .WithOne(o => o.Role);
        }
    }

    public class RoleOperationConfiguration : IEntityTypeConfiguration<RoleOperationEntity>
    {
        public void Configure(EntityTypeBuilder<RoleOperationEntity> builder)
        {
            builder.HasKey(ro => new { ro.IdRole, ro.IdOperation });

            builder
                .HasOne(ro => ro.Role)
                .WithMany(r => r.RoleOperations)
                .HasForeignKey(r => r.IdRole);
            builder
                .HasOne(ro => ro.Operation)
                .WithMany(o => o.RoleOperations)
                .HasForeignKey(ro => ro.IdOperation);
            
        }
    }
}
