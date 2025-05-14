using kursa4.Interfaces;
using kursa4.Models;
using Moq;
using System.Collections.Generic;

namespace kursa4.Mocks
{
    public class MockUser
    {
        public static Mock<IUser> GetMockUser()
        {
            var mockUser = new Mock<IUser>();

            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", FullName = "User One", PhoneNumber = "1234567890", Role = "Admin" }
            };

            // Настройка методов mock-объекта

            // Метод для получения всех пользователей
            mockUser.Setup(repo => repo.GetAllUsersAsync())
                .ReturnsAsync(users);

            // Метод для получения пользователя по ID
            mockUser.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => users.Find(user => user.Id == id));

            // Метод для получения пользователя по email
            mockUser.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.Find(user => user.Email == email));

            // Метод для добавления нового пользователя
            mockUser.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Метод для обновления пользователя
            mockUser.Setup(repo => repo.UpdateUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Метод для удаления пользователя
            mockUser.Setup(repo => repo.DeleteUserAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Метод для аутентификации пользователя
            mockUser.Setup(repo => repo.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string email, string password) => 
                    users.Find(user => user.Email == email && user.Password == password));

            return mockUser;
        }
    }
}