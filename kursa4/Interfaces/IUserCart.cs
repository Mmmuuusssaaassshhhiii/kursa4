using kursa4.Models;

namespace kursa4.Interfaces;

public interface IUserCart
{
    //work with cart
    Cart GetCartByUserId(int userId);
    void ClearCart(int userId);
    
    //work with items in the cart
    IEnumerable<CartItem> GetCartItems(int userId);
    void AddItemToCart(int userId, int laptopId, int quantity);
    void UpdateItemQuantity(int userId, int itemId, int newQuantity);
    void RemoveItemFromCart(int userId, int itemId);
}