using kursa4.Models;

namespace kursa4.Interfaces;

public interface IUsersOrders
{
    //work with orders
    Order GetOrderById(int orderId);
    IEnumerable<Order> GetOrderByUserId(int userId);
    void CreateOrder(Order order);
    void UpdateOrderStatus(int orderId, string status);
    void CancelOrder(int orderId);
    
    //work with orders items
    IEnumerable<OrderItem> GetOrderItems(int orderId);
    OrderItem GetOrderItemById(int orderItemId);
    
    //work with orders payment
    Payment GetPaymentByOrderId(int orderId);
    void ProcessPayment(int orderId, decimal amount);
    void UpdatePaymentStatus(int orderId, string status);
    
}