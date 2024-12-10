using Microsoft.AspNetCore.Components.Forms;

namespace Курсова_робота.Models.Entities
{
    public class PrintOrderEntity
    {
        public int Id { get; set; }
        public int IdEdition { get; set; }
        public EditionEntity Edition { get; set; }
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        public OrderDetailsEntity OrderDetail { get; set; }
    }
}
