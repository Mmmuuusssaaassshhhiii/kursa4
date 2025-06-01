using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class UserRepository : IAllUsers
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> Users => _context.Users;

    public User GetUser(int userId)
    {
        return _context.Users.SingleOrDefault(u => u.Id == userId);
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.SingleOrDefault(u => u.Email == email);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public bool Register(string email, string password, string fullName, string phoneNumber)
    {
        if (_context.Users.Any(u => u.Email == email))
            return false;

        var newUser = new User
        {
            Email = email,
            Password = password,
            FullName = fullName,
            PhoneNumber = phoneNumber,
            Role = "User"
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();
        return true;
    }

    public User Login(string email, string password)
    {
        return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
    }

    public void Logout(int userId)
    {
        // Здесь можно очистить токены, сессии и т.д. если нужно
    }
}
