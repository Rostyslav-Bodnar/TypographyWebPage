using System;
using System.ComponentModel.DataAnnotations;

namespace Курсова_робота.Models.ViewModels
{
    public class OrderAdvertisingViewModel
    {
        [Required(ErrorMessage = "URL-address is required")]
        [Url(ErrorMessage = "Please enter a valid URL address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select an end date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
