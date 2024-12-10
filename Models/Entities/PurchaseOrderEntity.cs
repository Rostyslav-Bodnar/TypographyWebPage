namespace Курсова_робота.Models.Entities
{
    public class PurchaseOrderEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int IdOrderStatus { get; set; }
        public OrderStatusEntity OrderStatus { get; set; }
        // Зв'язок із користувачами
        public int IdUser { get; set; }
        public UserEntity User { get; set; }

        // Зв'язок із поштовими відділеннями
        public int IdPostOffice { get; set; }
        public PostOfficeEntity PostOffice { get; set; }

        public string City { get; set; }

        // Зв'язок із типом оплати
        public int IdPaymentType { get; set; }
        public PaymentTypeEntity PaymentType { get; set; }

        public string? CardNumber { get; set; }
        public string? CVV { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Колекція товарів у замовленні
        public ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}