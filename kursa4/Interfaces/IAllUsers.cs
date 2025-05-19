using kursa4.Models;

namespace kursa4.Interfaces;

public interface IAllUsers
{
    IEnumerable<User> Users { get; }
    User GetUser(int userId);
    User GetUserByEmail(string email);
    
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int userId);

    // Дополнительные методы:
    bool Register(string email, string password, string fullName, string phoneNumber);
    User Login(string email, string password);
    void Logout(int userId); // если ведёшь учёт сессий
}
