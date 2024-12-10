using Microsoft.AspNetCore.Mvc;
using Курсова_робота.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Controllers
{
    public class OrderAdvertisingController : Controller
    {
        private readonly DB _context;

        public OrderAdvertisingController(DB context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult OrderAdvertising()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderAdvertising(OrderAdvertisingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new AdvertisingOrdersEntity
                {
                    CustomerEmail = model.Email,
                    URL = model.Address,
                    ViewDate = model.EndDate
                };

                _context.AdvertisingOrders.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
