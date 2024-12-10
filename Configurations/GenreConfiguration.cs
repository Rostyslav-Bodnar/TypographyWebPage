using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<GenreEntity>
    {
        public void Configure(EntityTypeBuilder<GenreEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder
                .HasMany(g => g.Editions)
                .WithOne(e => e.Genre)
                .HasForeignKey(e => e.IdGenre);
        }
    }

    public class TypeConfiguration : IEntityTypeConfiguration<TypeEntity>
    {
        public void Configure(EntityTypeBuilder<TypeEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder
                .HasMany(g => g.Editions)
                .WithOne(e => e.TypeEntity)
                .HasForeignKey(e => e.IdType);
        }
    }
}
