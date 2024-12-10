using System.ComponentModel.DataAnnotations.Schema;

namespace Курсова_робота.Models.Entities
{
    public class OrderDetailsEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int IdOrderStatus { get; set; }
        public OrderStatusEntity OrderStatus { get; set; }
    }
}
