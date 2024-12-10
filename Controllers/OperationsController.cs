using Microsoft.AspNetCore.Mvc;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Controllers
{
    public class OperationsController : Controller
    {
        private readonly DB _context;

        public OperationsController(DB context)
        {
            _context = context;
        }

        public IActionResult UserManagement() 
        {
            return View();
        }
    }

}
