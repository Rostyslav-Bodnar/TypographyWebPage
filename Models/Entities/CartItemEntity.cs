namespace Курсова_робота.Models.Entities
{
    public class CartItemEntity
    {
        public int Id { get; set; }
        public int EditionId { get; set; }
        public EditionEntity Edition { get; set; } // Навігаційна властивість
        public int Quantity { get; set; } = 1;
        public string UserId { get; set; } // Ідентифікатор користувача
    }
}
