using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Курсова_робота.Models.Entities;
using Курсова_робота.Services;

namespace Курсова_робота.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : Controller
    {
        private readonly DB _context;

        public ProductDetailsController(DB database)
        {
            _context = database;
        }
        public IActionResult ProductDetails(int id, string title)
        {
            var book = _context.Editions
                .FirstOrDefault(b => b.Id == id);
            var genre = _context.Genres.FirstOrDefault(g => g.Id == book.IdGenre);
            book.Genre = genre;
            if (book == null || book.Title.Replace(" ", "-") != title)
            {
                return NotFound();
            }

            return View(book);
        }

    }
}
