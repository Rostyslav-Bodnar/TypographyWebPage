using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // Для Include
using Курсова_робота.Models.Entities;
using Курсова_робота.Models.ViewModels;

namespace Курсова_робота.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly DB _context;

        public OrderManagementController(DB context)
        {
            _context = context;
        }

        public IActionResult OrderManagement(int? selectedOrderId = null)
        {
            // Завантажуємо PurchaseOrder з пов'язаними даними (OrderStatus, User, OrderItems)
            var orders = _context.PurchaseOrder
                .Include(po => po.OrderStatus)   // Завантажуємо OrderStatus
                .Include(po => po.User)          // Завантажуємо User
                .Include(po => po.OrderItems)    // Завантажуємо OrderItems
                .ThenInclude(oi => oi.Edition)
                .Include(po => po.PostOffice)
                .ToList();                       // Виконуємо запит та отримуємо результат

            // Створюємо список для відображення в ViewModel
            List<OrderManagementViewModel> list = orders.Select(order => new OrderManagementViewModel
            {
                PurchaseOrderEntity = order,
            }).ToList();

            // Завантажуємо деталі замовлення, якщо вибрано
            if (selectedOrderId.HasValue)
            {
                var selectedOrder = orders.FirstOrDefault(o => o.Id == selectedOrderId.Value);

                if (selectedOrder != null)
                {
                    bool canConfirm = true;
                    foreach (var item in selectedOrder.OrderItems)
                    {
                        if (item.Edition == null || item.Edition.Stock < item.Quantity)
                        {
                            canConfirm = false;
                            break;
                        }
                    }
                    // Заповнюємо ViewBag для відображення деталей замовлення
                    ViewBag.OrderDetails = new OrderManagementDetailsViewModel
                    {
                        OrderId = selectedOrder.Id,
                        Username = selectedOrder.User.Username,
                        PaymentType = selectedOrder.IdPaymentType,
                        CardNumber = selectedOrder.CardNumber ?? "N/A",
                        CVV = selectedOrder.CVV,
                        ExpirationDate = selectedOrder.ExpirationDate.ToString(),
                        PostOffice = selectedOrder.PostOffice.Name,
                        City = selectedOrder.City ?? "N/A",
                        IdOrderStatus = selectedOrder.IdOrderStatus,
                        Items = selectedOrder.OrderItems.Select(oi => new OrderItemViewModel
                        {
                            Title = oi.Edition?.Title ?? "No Title",
                            Quantity = oi.Quantity
                            
                        }).ToList(),
                        CanConfirm = canConfirm
                    };
                }
            }

            return View("~/Views/Operations/OrderManagement.cshtml", list);
        }

        [HttpPost]
        public IActionResult ConfirmAndSend(int id)
        {
            // Знайдемо замовлення за його ID з деталями
            var order = _context.PurchaseOrder
                .Include(po => po.OrderStatus)
                .Include(po => po.OrderItems) // Завантажуємо товари замовлення
                .ThenInclude(oi => oi.Edition) // Завантажуємо дані про видання
                .FirstOrDefault(po => po.Id == id);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            // Перевіряємо наявність достатньої кількості товарів
            foreach (var item in order.OrderItems)
            {
                if (item.Edition == null)
                {
                    return BadRequest($"Edition for item ID {item.Id} is not found.");
                }

                if (item.Edition.Stock < item.Quantity)
                {
                    return BadRequest($"Not enough stock for {item.Edition.Title}. Available: {item.Edition.Stock}, Requested: {item.Quantity}.");
                }
            }

            // Віднімаємо кількість товарів із бази даних
            foreach (var item in order.OrderItems)
            {
                item.Edition.Stock -= item.Quantity;
            }

            // Змінюємо статус замовлення
            var confirmedStatus = _context.OrderStatus.FirstOrDefault(s => s.Id == 2);
            if (confirmedStatus != null)
            {
                order.OrderStatus = confirmedStatus;
                _context.SaveChanges();
            }

            return RedirectToAction("OrderManagement");
        }


    }
}
