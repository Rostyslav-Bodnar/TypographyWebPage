using Курсова_робота.Models.Entities;

namespace Курсова_робота.Models.ViewModels
{
    public class OrderManagementViewModel
    {
        public PurchaseOrderEntity PurchaseOrderEntity { get; set; }

    }

    public class OrderManagementDetailsViewModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public string? CardNumber { get; set; }
        public string City { get; set; }
        public string PostOffice { get; set; }
        public int PaymentType { get; set; }
        public string? CVV { get; set; }
        public string? ExpirationDate { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        public int IdOrderStatus { get; set; }

        public bool CanConfirm {  get; set; } 
    }

    public class OrderItemViewModel
    {
        public string Title { get; set; }
        public int Quantity { get; set; }

    }
}
