using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockUser : IAllUsers
{
    private List<User> _users = new List<User>
    {
        new User
        {
            Id = 1,
            Email = "test@test.com",
            Password = "test",
            FullName = "Pidor",
            PhoneNumber = "123456",
            Role = "User",
        },
        new User
        {
            Id = 2,
            Email = "admin@example.com",
            Password = "adminpass",
            FullName = "Admin User",
            PhoneNumber = "0987654321",
            Role = "Admin"
        }
    };
    
    public IEnumerable<User> Users => _users;

    public User GetUser(int userId)
    {
        return _users.FirstOrDefault(u => u.Id == userId);
    }

    public User GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public void AddUser(User user)
    {
        user.Id = _users.Max(u => u.Id) + 1;
        _users.Add(user);
    }

    public void UpdateUser(User user)
    {
        var existingUser = GetUser(user.Id);
        if (existingUser != null)
        {
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.FullName = user.FullName;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Role = user.Role;
        }
    }

    public void DeleteUser(int userId)
    {
        var user = GetUser(userId);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}