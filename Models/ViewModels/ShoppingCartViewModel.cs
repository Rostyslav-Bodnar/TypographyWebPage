using Курсова_робота.Models.Entities;

namespace Курсова_робота.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<CartItemEntity> CartItems { get; set; }
        public IEnumerable<PostOfficeEntity> PostOffices { get; set; }
        public IEnumerable<PaymentTypeEntity> PaymentTypes { get; set; }
        public int? SelectedPaymentTypeId { get; set; } // Для відображення вибору
        public int? SelectedPostOffice { get; set; } // Для відображення вибору

    }
}

