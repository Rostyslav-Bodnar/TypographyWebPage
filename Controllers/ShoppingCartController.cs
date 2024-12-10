using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Курсова_робота.Models.Entities;
using Курсова_робота.Models.ViewModels;
using Курсова_робота.Services;

public class ShoppingCartController : Controller
{
    private readonly DB _context;
    private readonly CartService _cartService;

    public ShoppingCartController(DB context, CartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    public IActionResult ShoppingCart()
    {
        var userId = User.Identity.Name; // Отримання ідентифікатора користувача
        var cartItems = _cartService.GetCartItems(userId);

        var viewModel = new OrderViewModel
        {
            CartItems = cartItems
        };

        ViewBag.PaymentTypes = _context.PaymentType.ToList();
        ViewBag.PostOffice = _context.PostOffice.ToList();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        var userId = User.Identity.Name; // Отримання ідентифікатора користувача
        _cartService.AddToCart(userId, productId);
        return RedirectToAction("ShoppingCart");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        var userId = User.Identity.Name;
        _cartService.RemoveFromCart(userId, productId);
        return RedirectToAction("ShoppingCart");
    }

    [HttpPost]
    public IActionResult ClearCart()
    {
        var userId = User.Identity.Name;
        _cartService.ClearCart(userId);
        return RedirectToAction("ShoppingCart");
    }

    [HttpPost]
    public IActionResult UpdateCart(Dictionary<int, int> quantities)
    {
        var userId = User.Identity.Name;

        foreach (var quantity in quantities)
        {
            _cartService.UpdateQuantity(userId, quantity.Key, quantity.Value);
        }
        return RedirectToAction("ShoppingCart");
    }


    [HttpPost]
    public IActionResult ConfirmOrder(OrderViewModel viewModel)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
        var IdUser = User.Identity.Name;
        if (user == null)
        {
            throw new Exception("Користувач не знайдений.");
        }
        var userId = user.Id;

        // Створення самого замовлення
        var purchaseOrder = new PurchaseOrderEntity
        {
            IdUser = userId,
            OrderDate = DateTime.Now,
            IdOrderStatus = 3,
            IdPostOffice = viewModel.SelectedPostOfficeId,
            City = viewModel.City,
            IdPaymentType = viewModel.SelectedPaymentTypeId,
            TotalPrice = 0, // Тимчасово, обчислимо пізніше
            CardNumber = viewModel.SelectedPaymentTypeId == 2 ? viewModel.CardNumber : null,
            CVV = viewModel.SelectedPaymentTypeId == 2 ? viewModel.CVV : null,
            ExpirationDate = viewModel.SelectedPaymentTypeId == 2 ? viewModel.ExpirationDate : null
        };

        _context.PurchaseOrder.Add(purchaseOrder);
        _context.SaveChanges();

        // Отримання елементів із кошика
        var cartItems = _cartService.GetCartItems(user.Username);
        decimal totalPrice = 0;
        if (!cartItems.Any())
        {
            ModelState.AddModelError("", "Кошик порожній.");
            return RedirectToAction("Home");
        }

        foreach (var cartItem in cartItems)
        {
            var edition = _context.Editions.FirstOrDefault(e => e.Id == cartItem.Edition.Id);
            if (edition == null)
            {
                ModelState.AddModelError("", $"Товар із ID {cartItem.Edition.Id} не знайдено.");
                continue;
            }

            // Перевірка кількості на складі
            if (cartItem.Quantity > edition.Stock)
            {
                ModelState.AddModelError("", $"Недостатньо товару {edition.Title} на складі.");
                return RedirectToAction("ShoppingCart");
            }

            // Створення елементів замовлення
            var orderItem = new OrderItemEntity
            {
                IdEdition = cartItem.Edition.Id,
                Quantity = cartItem.Quantity,
                Price = cartItem.Edition.Price * cartItem.Quantity,
                IdPurchaseOrder = purchaseOrder.Id // Зв’язок із PurchaseOrder
            };

            totalPrice += cartItem.Quantity * cartItem.Edition.Price;
            _context.OrderItems.Add(orderItem);

            // Зменшення кількості на складі
            edition.Stock -= cartItem.Quantity;

            // Перевірка, чи потрібно створити PrintOrder
            if (edition.Stock < 10)
            {
                var printOrder = new PrintOrderEntity
                {
                    IdEdition = edition.Id,
                    Edition = edition,
                    Quantity = 100,
                    OrderDetail = new OrderDetailsEntity
                    {
                        OrderDate = DateTime.Now,
                        IdOrderStatus = 3
                    }
                };

                _context.PrintOrders.Add(printOrder);
            }
        }

        // Оновлення загальної ціни замовлення
        purchaseOrder.TotalPrice = totalPrice;
        _context.PurchaseOrder.Update(purchaseOrder);

        _context.SaveChanges();

        // Очищення кошика після підтвердження замовлення
        _cartService.ClearCart(IdUser);

        return RedirectToAction("Index", "Home");
    }



}

