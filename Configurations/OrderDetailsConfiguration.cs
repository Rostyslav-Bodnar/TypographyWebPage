using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailsEntity> builder)
        {
            builder.HasKey(od => od.Id);

            builder
            .HasOne(o => o.OrderStatus)
            .WithMany() // Якщо це однонапрямлений зв'язок
            .HasForeignKey(o => o.IdOrderStatus)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }

    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.HasKey(oi => oi.Id);

            // Зв'язок із EditionEntity
            builder
                .HasOne(oi => oi.Edition)
                .WithMany() // Якщо у EditionEntity немає колекції OrderItems
                .HasForeignKey(oi => oi.IdEdition)
                .OnDelete(DeleteBehavior.Restrict);

            // Зв'язок із PurchaseOrder
            builder
                .HasOne(oi => oi.PurchaseOrder)
                .WithMany(po => po.OrderItems)
                .HasForeignKey(oi => oi.IdPurchaseOrder)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrderEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderEntity> builder)
        {
            builder.HasKey(po => po.Id);

            // Зв'язок із UserEntity
            builder
                .HasOne(po => po.User)
                .WithMany(u => u.PurchaseOrderEntities)
                .HasForeignKey(po => po.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            // Зв'язок із PostOfficeEntity
            builder
                .HasOne(po => po.PostOffice)
                .WithMany()
                .HasForeignKey(po => po.IdPostOffice)
                .OnDelete(DeleteBehavior.Restrict);

            // Зв'язок із PaymentTypeEntity
            builder
                .HasOne(po => po.PaymentType)
                .WithMany()
                .HasForeignKey(po => po.IdPaymentType)
                .OnDelete(DeleteBehavior.Restrict);

            // Зв'язок із OrderItems
            builder
                .HasMany(po => po.OrderItems)
                .WithOne(oi => oi.PurchaseOrder)
                .HasForeignKey(oi => oi.IdPurchaseOrder)
                .OnDelete(DeleteBehavior.Cascade);

            builder.
                HasOne(po => po.OrderStatus)
                .WithMany()
                .HasForeignKey(po => po.IdOrderStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PrintOrderConfiguration : IEntityTypeConfiguration<PrintOrderEntity>
    {
        public void Configure(EntityTypeBuilder<PrintOrderEntity> builder)
        {
            builder
                .HasOne(po => po.Edition)
                .WithMany()
                .HasForeignKey(po => po.IdEdition)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(po => po.OrderDetail)
                .WithMany()
                .HasForeignKey(po => po.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
