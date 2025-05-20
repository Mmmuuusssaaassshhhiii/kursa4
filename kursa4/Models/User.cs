namespace kursa4.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = "user"; // например, по умолчанию

    public Cart? Cart { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }

    public User()
    {
        Orders = new List<Order>();
        Reviews = new List<Review>();
    }
}