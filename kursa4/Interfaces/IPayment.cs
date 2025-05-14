using kursa4.Models;

namespace kursa4.Interfaces;

public interface IPayment
{
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);
    Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(int orderId);
    Task AddPaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
    Task DeletePaymentAsync(int id);
    Task<Payment> GetLatestPaymentByOrderIdAsync(int orderId);
}