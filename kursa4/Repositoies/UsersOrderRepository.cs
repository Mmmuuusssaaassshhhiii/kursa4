using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class UsersOrderRepository : IUsersOrders
{
    private readonly List<Order> _orders = new();
    private readonly List<OrderItem> _orderItems = new();
    private readonly List<Payment> _payments = new();
    private int _orderIdCounter = 1;
    private int _orderItemIdCounter = 1;
    private int _paymentIdCounter = 1;

    public Order GetOrderById(int orderId)
    {
        return _orders.FirstOrDefault(o => o.Id == orderId);
    }

    public IEnumerable<Order> GetOrderByUserId(int userId)
    {
        return _orders.Where(o => o.UserId == userId);
    }

    public void CreateOrder(Order order)
    {
        order.Id = _orderIdCounter++;
        order.OrderDate = DateTime.Now;
        order.Status = "Pending";

        foreach (var item in order.OrderItems)
        {
            item.Id = _orderItemIdCounter++;
            item.OrderId = order.Id;
            _orderItems.Add(item);
        }
        
        _orders.Add(order);

        var payment = new Payment
        {
            Id = _paymentIdCounter++,
            OrderId = order.Id,
            Amount = order.OrderItems.Sum(o => o.Price * o.Quantity),
            Status = "Pending",
            PaymentDate = DateTime.Now
        };
        _payments.Add(payment);
        
        order.Payment = payment;
    }

    public void UpdateOrderStatus(int orderId, string status)
    {
        var order = GetOrderById(orderId);
        if (order != null)
        {
            order.Status = status;
        }
    }

    public void CancelOrder(int orderId)
    {
        var order = GetOrderById(orderId);
        if (order != null)
        {
            order.Status = "Cancelled";
        }
    }
    
    public IEnumerable<OrderItem> GetOrderItems(int orderId)
    {
        return _orderItems.Where(oi => oi.OrderId == orderId);
    }

    public OrderItem GetOrderItemById(int orderItemId)
    {
        return _orderItems.FirstOrDefault(oi => oi.Id == orderItemId);
    }

    public Payment GetPaymentByOrderId(int orderId)
    {
        return _payments.FirstOrDefault(p => p.OrderId == orderId);
    }

    public void ProcessPayment(int orderId, decimal amount)
    {
        var payment = GetPaymentByOrderId(orderId);
        if (payment != null)
        {
            payment.Amount = amount;
            payment.Status = "paid";
            payment.PaymentDate = DateTime.Now;
        }
    }

    public void UpdatePaymentStatus(int orderId, string status)
    {
        var payment = GetPaymentByOrderId(orderId);
        if (payment != null)
        {
            payment.Status = status;
        }
    }
}