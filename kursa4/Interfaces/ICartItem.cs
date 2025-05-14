using kursa4.Models;

namespace kursa4.Interfaces;

public interface ICartItem
{
    Task<CartItem> GetCartItemByIdAsync(int id);
    Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
    Task AddCartItemAsync(CartItem cartItem);
    Task UpdateCartItemAsync(CartItem cartItem);
    Task DeleteCartItemAsync(int id);
    Task DeleteCartItemsByCartIdAsync(int cartId);
}