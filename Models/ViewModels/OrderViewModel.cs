using Microsoft.AspNetCore.Mvc.Rendering;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Models.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<CartItemEntity> CartItems { get; set; }
        public int UserId { get; set; }
        public int SelectedPostOfficeId { get; set; }
        public string City { get; set; }
        public int SelectedPaymentTypeId { get; set; }
        public string? CardNumber { get; set; }
        public string? CVV { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}


