using kursa4.Interfaces;
using kursa4.Models;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Repositories;

public class UsersOrderRepository : IUsersOrders
{
    private readonly ApplicationDbContext _context;

    public UsersOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Order GetOrderById(int orderId)
    {
        return _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.Payment)
            .FirstOrDefault(o => o.Id == orderId);
    }

    public IEnumerable<Order> GetOrderByUserId(int userId)
    {
        return _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.Payment)
            .Where(o => o.UserId == userId)
            .ToList();
    }

    public void CreateOrder(Order order)
    {
        order.OrderDate = DateTime.Now;
        order.Status = "В ожидании";

        _context.Orders.Add(order);
        _context.SaveChanges();

        var payment = new Payment
        {
            OrderId = order.Id,
            Amount = order.OrderItems.Sum(o => o.Price * o.Quantity),
            Status = "В ожидании",
            PaymentDate = DateTime.Now
        };

        _context.Payments.Add(payment);
        _context.SaveChanges();

        order.Payment = payment;
    }

    public void UpdateOrderStatus(int orderId, string status)
    {
        var order = _context.Orders.Find(orderId);
        if (order != null)
        {
            order.Status = status;
            _context.SaveChanges();
        }
    }

    public void CancelOrder(int orderId)
    {
        var order = _context.Orders.Find(orderId);
        if (order != null)
        {
            order.Status = "Отменён";
            _context.SaveChanges();
        }
    }

    public IEnumerable<OrderItem> GetOrderItems(int orderId)
    {
        return _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Include(oi => oi.Laptop)
            .ToList();
    }

    public OrderItem GetOrderItemById(int orderItemId)
    {
        return _context.OrderItems
            .Include(oi => oi.Laptop)
            .FirstOrDefault(oi => oi.Id == orderItemId);
    }

    public Payment GetPaymentByOrderId(int orderId)
    {
        return _context.Payments
            .FirstOrDefault(p => p.OrderId == orderId);
    }

    public void ProcessPayment(int orderId, decimal amount)
    {
        var payment = GetPaymentByOrderId(orderId);
        if (payment != null)
        {
            payment.Amount = amount;
            payment.Status = "Оплачено";
            payment.PaymentDate = DateTime.Now;
            _context.SaveChanges();
        }
    }

    public void UpdatePaymentStatus(int orderId, string status)
    {
        var payment = GetPaymentByOrderId(orderId);
        if (payment != null)
        {
            payment.Status = status;
            _context.SaveChanges();
        }
    }
}
