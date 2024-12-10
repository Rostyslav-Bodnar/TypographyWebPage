using Microsoft.EntityFrameworkCore;
using Курсова_робота.Configurations;

namespace Курсова_робота.Models.Entities
{
    public class DB : DbContext
    {

        public DB(DbContextOptions<DB> options) : base(options){ }

        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<EditionEntity> Editions { get; set; }
        public DbSet<OperationEntity> Operations { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<RoleOperationEntity> RoleOperations { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderStatusEntity> OrderStatus { get; set; }
        public DbSet<OrderDetailsEntity> OrderDetails { get; set; }
        public DbSet<PrintOrderEntity> PrintOrders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<PurchaseOrderEntity> PurchaseOrder { get; set; }
        public DbSet<TypographyStatusEntity> TypographyStatus { get; set; }
        public DbSet<TypographyEntity> Typography { get; set; }
        public DbSet<AdvertisingOrdersEntity> AdvertisingOrders { get; set; }

        public DbSet<PaymentTypeEntity> PaymentType { get; set; }
        public DbSet<PostOfficeEntity> PostOffice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new EditionConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new OperationConfiguration());
            modelBuilder.ApplyConfiguration(new RoleOperationConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemsConfiguration());
            modelBuilder.ApplyConfiguration(new TypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypographyConfiguration());
            modelBuilder.ApplyConfiguration(new TypographyStatusConfiguration());
            modelBuilder.ApplyConfiguration(new PrintOrderConfiguration());



            base.OnModelCreating(modelBuilder);
        }

    }
}
