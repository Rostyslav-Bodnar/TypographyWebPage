using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Курсова_робота.Models.Entities;
using Курсова_робота.Models.ViewModels;

namespace Курсова_робота.Controllers
{
    public class UserManagementController : Controller
    {
        public DB _context;

        public UserManagementController(DB context)
        {
            _context = context;
        }

        public IActionResult UserManagement()
        {
            var users = _context.Users
                .Include(u => u.Role)
                .ToList(); // Отримати список користувачів
            var roles = _context.Roles // Отримати список ролей
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                })
                .ToList();

            ViewBag.Roles = roles; // Передаємо список ролей у ViewBag
            ViewBag.IsAdmin = User.IsInRole("1");
            return View("~/Views/Operations/UserManagement.cshtml", users); // Явно вказуємо шлях до представлення
        }

        [HttpPost]
        public IActionResult UpdateRole(int userId, int roleId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            user.IdRole = roleId; // Оновлюємо роль
            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(UserManagement)); // Повертаємося до списку користувачів
        }


        // Видалити користувача
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); // Переходимо до списку користувачів
        }

        [HttpPost]
        // Заблокувати/розблокувати користувача
        public IActionResult ToggleBan(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsBanned = !user.IsBanned; // Змінюємо статус блокування
            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("UserManagement");
        }
    }
}
