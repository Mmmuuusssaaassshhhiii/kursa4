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
}