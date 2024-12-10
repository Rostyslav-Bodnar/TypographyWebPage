using Microsoft.AspNetCore.Mvc;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Controllers
{
    public class AssignedAdsController : Controller
    {
        public DB _context;

        public AssignedAdsController(DB context)
        {
            _context = context;
        }
        public IActionResult AssignedAds()
        {
            var ads = _context.AdvertisingOrders.ToList();
            return View("~/Views/Operations/AssignedAds.cshtml", ads);
        }

        [HttpPost]
        public async Task<IActionResult> AddDesign(int id, IFormFile designImage)
        {
            var ad = await _context.AdvertisingOrders.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }

            if (designImage != null && designImage.Length > 0)
            {
                // Зберігаємо зображення у wwwroot/media/ads
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media/ads");
                Directory.CreateDirectory(uploadsFolder); // Переконайтесь, що директорія існує

                var fileName = $"{Guid.NewGuid()}_{designImage.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await designImage.CopyToAsync(stream);
                }

                // Оновлюємо запис у базі даних
                ad.ImagePath = $"/media/ads/{fileName}";
                ad.isHavingDesign = true;

                _context.Update(ad);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(AssignedAds)); // Повертаємось до списку
        }

    }
}
