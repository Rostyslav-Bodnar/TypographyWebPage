using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Configurations
{
    public class EditionConfiguration : IEntityTypeConfiguration<EditionEntity>
    {
        public void Configure(EntityTypeBuilder<EditionEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.
                HasOne(e => e.Genre)
                .WithMany(g => g.Editions)
                .HasForeignKey(e => e.IdGenre)
                .OnDelete(DeleteBehavior.Restrict);

            builder.
                HasOne(e => e.TypeEntity)
                .WithMany(t => t.Editions)
                .HasForeignKey(e => e.IdType)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
    public class TypographyConfiguration : IEntityTypeConfiguration<TypographyEntity>
    {
        public void Configure(EntityTypeBuilder<TypographyEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder.
                HasOne(t => t.TypographyStatus)
                .WithMany(ts => ts.Typographies)
                .HasForeignKey(t => t.IdTypographyStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TypographyStatusConfiguration : IEntityTypeConfiguration<TypographyStatusEntity>
    {
        public void Configure(EntityTypeBuilder<TypographyStatusEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder.
                HasMany(ts => ts.Typographies)
                .WithOne(t => t.TypographyStatus)
                .HasForeignKey(t => t.IdTypographyStatus);
        }
    }
}
