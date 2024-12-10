using Microsoft.AspNetCore.Mvc;
using Курсова_робота.Models.Entities;
using Курсова_робота.Models.ViewModels;

namespace Курсова_робота.Controllers
{
    public class SignUpController : Controller
    {
        private readonly DB _context;

        public SignUpController(DB context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Створюємо нового користувача
                var user = new UserEntity
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password, // У реальних проектах використовуйте хешування паролів!
                    IdRole = 3
                };

                // Додаємо користувача в базу даних
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("SignIn", "Auth");
            }

            return View(model);
        }
    }
}
