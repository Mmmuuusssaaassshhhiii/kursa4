using kursa4.Interfaces;
using kursa4.Models;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Mocks;

public class UserCartRepository : IUserCart
{
    private readonly ApplicationDbContext _context;

    public UserCartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Cart GetCartByUserId(int userId)
    {
        var cart = _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Laptop) // ← это важно!
            .FirstOrDefault(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>()
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        return cart;
    }


    public void ClearCart(int userId)
    {
        var cart = GetCartByUserId(userId);
        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            _context.SaveChanges();
        }
    }

    public IEnumerable<CartItem> GetCartItems(int userId)
    {
        return GetCartByUserId(userId).CartItems;
    }

    public void AddItemToCart(int userId, int laptopId, int quantity)
    {
        var cart = GetCartByUserId(userId);
        var existingItem = cart.CartItems.FirstOrDefault(i => i.LaptopId == laptopId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new CartItem
            {
                CartId = cart.Id,
                LaptopId = laptopId,
                Quantity = quantity
            };
            _context.CartItems.Add(newItem);
        }

        _context.SaveChanges(); // Не забудь сохранить!
    }

    public void UpdateItemQuantity(int userId, int itemId, int quantity)
    {
        var cart = GetCartByUserId(userId);
        var item = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.Quantity = quantity;
            _context.SaveChanges();
        }
    }

    public void RemoveItemFromCart(int userId, int itemId)
    {
        var cart = GetCartByUserId(userId);
        var item = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }
    }
    
    public Laptop GetLaptopById(int laptopId)
    {
        return _context.Laptops.FirstOrDefault(l => l.Id == laptopId);
    }
}
