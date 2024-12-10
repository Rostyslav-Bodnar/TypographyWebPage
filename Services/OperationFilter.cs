using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Курсова_робота.Services
{ 

    public class OperationsFilter : IAsyncActionFilter
    {
        private readonly IMenuService _menuService;

        public OperationsFilter(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var roleId = int.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "0");
            // Завантажуємо операції і додаємо їх у ViewData
            var operations = await _menuService.GetOperationsByRoleAsync(roleId); // Припустимо, цей метод повертає список операцій
            var controller = context.Controller as Controller;
            controller.ViewData["Operations"] = operations;

            await next(); // Продовжуємо виконання дії
        }
    }
}
