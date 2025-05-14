using kursa4.Models;

namespace kursa4.Interfaces;

public interface IOrderItem
{
    Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
    Task<OrderItem> GetOrderItemByIdAsync(int id);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    Task AddOrderItemAsync(OrderItem orderItem);
    Task UpdateOrderItemAsync(OrderItem orderItem);
    Task DeleteOrderItemAsync(int id);
}