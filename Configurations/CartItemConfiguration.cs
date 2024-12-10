using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder
                .HasOne(i => i.Edition)
                .WithMany()
                .HasForeignKey(i => i.EditionId);
        }
    }
}
