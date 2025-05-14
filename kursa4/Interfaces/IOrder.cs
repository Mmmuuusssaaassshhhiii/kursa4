using kursa4.Models;

namespace kursa4.Interfaces;

public interface IOrder
{
    // Получить все заказы
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    // Получить заказ по ID
    Task<Order> GetOrderByIdAsync(int id);

    // Создать новый заказ
    Task<Order> CreateOrderAsync(Order order);

    // Обновить существующий заказ
    Task<Order> UpdateOrderAsync(Order order);

    // Удалить заказ
    Task<bool> DeleteOrderAsync(int id);

    // Получить заказы для конкретного пользователя
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

    // Получить заказы с фильтрацией по статусу
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);

    // Изменить статус заказа
    Task<Order> UpdateOrderStatusAsync(int orderId, string newStatus);

    // Обработать оплату заказа
    Task<Order> ProcessPaymentAsync(int orderId, Payment payment);
}