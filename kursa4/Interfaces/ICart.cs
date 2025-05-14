using kursa4.Models;

namespace kursa4.Interfaces;

public interface ICart
{
    Task<Cart> GetCartByUserIdAsync(int userId);
    Task AddCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task DeleteCartAsync(int id);
    Task AddCartItemAsync(int cartId, CartItem cartItem);
    Task RemoveCartItemAsync(int cartId, int cartItemId);
    Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);
}