using Microsoft.EntityFrameworkCore;
using Курсова_робота.Models.Entities;

namespace Курсова_робота.Services
{
    public class CartService
    {
        private readonly DB _context;

        public CartService(DB context)
        {
            _context = context;
        }

        public List<CartItemEntity> GetCartItems(string userId)
        {
            return _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Edition) // Завантаження пов'язаних даних про видання
                .ToList();
        }

        public void AddToCart(string userId, int productId)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.EditionId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                _context.CartItems.Add(new CartItemEntity
                {
                    UserId = userId,
                    EditionId = productId,
                    Quantity = 1
                });
            }
            _context.SaveChanges();
        }

        public void UpdateQuantity(string userId, int productId, int quantity)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.EditionId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }
        }



        public void RemoveFromCart(string userId, int productId)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.EditionId == productId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void ClearCart(string userId)
        {
            var cartItems = _context.CartItems.Where(c => c.UserId == userId);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
    }
}
