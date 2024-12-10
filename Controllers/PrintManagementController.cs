using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Controllers
{
    public class PrintManagementController : Controller
    {
        private readonly DB _context;

        public PrintManagementController(DB context)
        {
            _context = context;
        }

        // Відображення сторінки з типографіями та замовленнями
        public IActionResult PrintManagement()
        {
            // Завантажуємо всі типографії
            var typography = _context.Typography
                .Include(t => t.TypographyStatus) // Завантаження статусу
                .ToList();

            // Завантажуємо всі замовлення на друк
            var printOrders = _context.PrintOrders
                .Include(po => po.Edition)      // Завантаження видання
                .Include(po => po.OrderDetail)
                .ThenInclude(od => od.OrderStatus)// Завантаження деталей замовлення
                .ToList();

            ViewBag.Typographies = typography;
            return View("~/Views/Operations/PrintManagement.cshtml", printOrders);
        }

        // Початок друку
        [HttpPost]
        public async Task<IActionResult> StartPrinting(int selectedEditionId)
        {
            // Отримати замовлення на друк
            var printOrder = _context.PrintOrders
                .Include(po => po.Edition) // Завантаження видання
                .Include(po => po.OrderDetail)
                    .ThenInclude(od => od.OrderStatus)
                .FirstOrDefault(po => po.Id == selectedEditionId);

            if (printOrder == null)
                return NotFound("Print order not found.");

            // Отримати типографію зі статусом "Ready to work"
            var typography = _context.Typography
                .FirstOrDefault(t => t.TypographyStatus.Id == 1);

            if (typography == null)
                return BadRequest("No available typography to handle the request.");

            // Оновлюємо статус типографії
            var busyStatus = _context.TypographyStatus.FirstOrDefault(ts => ts.Id == 2);
            var proccessOrderStatus = _context.OrderStatus.FirstOrDefault(os => os.Id == 3);
            if (busyStatus == null || proccessOrderStatus == null)
                return BadRequest("Typography or order status 'Busy' not found.");

            typography.TypographyStatus = busyStatus;
            typography.LastUpdated = DateTime.Now;

            printOrder.OrderDetail.OrderStatus = proccessOrderStatus;
            await _context.SaveChangesAsync();

            // Симуляція друку
            await Task.Delay(TimeSpan.FromMinutes(1));

            // Після друку:
            // 1. Змінюємо статус типографії на "Ready to work"
            var readyStatus = _context.TypographyStatus.FirstOrDefault(ts => ts.Id == 1);
            if (readyStatus != null)
            {
                typography.TypographyStatus = readyStatus;
                typography.LastUpdated = DateTime.Now;

            }

            // 2. Видаляємо замовлення на друк
            _context.PrintOrders.Remove(printOrder);

            // 3. Збільшуємо кількість видань
            if (printOrder.Edition != null)
            {
                printOrder.Edition.Stock += 100; // Збільшуємо на 100
            }

            // Зберігаємо всі зміни
            await _context.SaveChangesAsync();

            return RedirectToAction("PrintManagement");
        }
    }
}
