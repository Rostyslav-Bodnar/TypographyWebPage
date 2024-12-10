using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Controllers
{
    public class ContentManagementController : Controller
    {
        public DB _context;

        public ContentManagementController(DB context)
        {
            _context = context;
        }
        public IActionResult ContentManagement( int? selectedEditionId)
        {
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Types = _context.Types.ToList();
            var editions = _context.Editions
                .Include(e => e.Genre)
                .Include(e => e.TypeEntity)
                .ToList();

            if (selectedEditionId.HasValue)
            {
                var selectedEdition = editions.FirstOrDefault(e => e.Id == selectedEditionId);

                if (selectedEdition != null)
                {
                    ViewBag.EditionDetails = new EditionEntity
                    {
                        Id = selectedEdition.Id,
                        Title = selectedEdition.Title,
                        Author = selectedEdition.Author,
                        IdType = selectedEdition.IdType,
                        TypeEntity = selectedEdition.TypeEntity,
                        IdGenre = selectedEdition.IdGenre,
                        Description = selectedEdition.Description,
                        ReleaseDate = selectedEdition.ReleaseDate,
                        Pages = selectedEdition.Pages,
                        Genre = selectedEdition.Genre,
                        Price = selectedEdition.Price,
                        Stock = selectedEdition.Stock,
                    };
                }
            }


            return View("~/Views/Operations/ContentManagement.cshtml", editions); 
        }

        [HttpPost]
        public IActionResult RemoveEdition(int? selectedEditionId)
        {
            if(selectedEditionId.HasValue)
            {
                var edition = _context.Editions.FirstOrDefault(e => e.Id == selectedEditionId);
                if(edition != null)
                {
                    _context.Editions.Remove(edition);
                    _context.SaveChanges();
                    return View("ContentManagement");
                }
                else
                {
                    return NotFound();
                }

            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddEdition(EditionEntity edition, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Шлях до папки з медіа
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media/editions");
                Directory.CreateDirectory(uploadsFolder); // Переконайтесь, що директорія існує

                // Генерація унікальної назви для файлу
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Збереження файлу
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Збереження шляху до зображення у базу даних
                edition.ImagePath = $"/media/editions/{fileName}";
            }

            // Додавання нового видання у базу
            _context.Editions.Add(edition);
            await _context.SaveChangesAsync();

            return RedirectToAction("ContentManagement");
        }


    }
}
