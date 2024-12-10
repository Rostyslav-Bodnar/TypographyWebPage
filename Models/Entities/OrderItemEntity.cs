using Курсова_робота.Models.Entities;

public class OrderItemEntity
{
    public int Id { get; set; }
    // Зв'язок із виданнями
    public int IdEdition { get; set; }
    public EditionEntity Edition { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Зв'язок із замовленням (PurchaseOrder)
    public int IdPurchaseOrder { get; set; }
    public PurchaseOrderEntity PurchaseOrder { get; set; }
}
