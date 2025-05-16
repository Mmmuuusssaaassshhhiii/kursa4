using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockUserCart : IUserCart
{
    private readonly List<Cart> _carts = new List<Cart>();
    private int _itemIdCounter = 1;

    public Cart GetCartByUserId(int userId)
    {
        var cart = _carts.SingleOrDefault(c => c.UserId == userId);
        if (cart == null)
        {
            cart = new Cart
            {
                Id = _carts.Count + 1,
                UserId = userId,
                CartItems = new List<CartItem>()
            };
            _carts.Add(cart);
        }
        return cart;
    }

    public void ClearCart(int userId)
    {
        var cart = GetCartByUserId(userId);
        cart.CartItems.Clear();
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
            cart.CartItems.Add(new CartItem
            {
                Id = _itemIdCounter++,
                CartId = cart.Id,
                LaptopId = laptopId,
                Quantity = quantity,
            });
        }
    }

    public void UpdateItemQuantity(int userId, int laptopId, int quantity)
    {
        var cart = GetCartByUserId(userId);
        var item = cart.CartItems.FirstOrDefault(i => i.LaptopId == laptopId);
        if (item != null)
        {
            item.Quantity += quantity;
        }
    }

    public void RemoveItemFromCart(int userId, int itemId)
    {
        var cart = GetCartByUserId(userId);
        var item = cart.CartItems.FirstOrDefault(i => i.LaptopId == itemId);
        if (item != null)
        {
            cart.CartItems.Remove(item);
        }
    }
}
