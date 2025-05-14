using kursa4.Interfaces;
using kursa4.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kursa4.Mocks
{
    public class MockCartItem
    {
        public static Mock<ICartItem> GetMockCartItem()
        {
            var mockCartItem = new Mock<ICartItem>();

            var cartItems = new List<CartItem>
            {
                new CartItem { Id = 1, CartId = 1, LaptopId = 1, Quantity = 2 },
                new CartItem { Id = 2, CartId = 1, LaptopId = 2, Quantity = 1 }
            };

            // Настройка методов mock-объекта

            // Метод для получения элемента корзины по ID
            mockCartItem.Setup(repo => repo.GetCartItemByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cartItems.Find(item => item.Id == id));

            // Метод для получения всех элементов корзины по cartId
            mockCartItem.Setup(repo => repo.GetCartItemsByCartIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int cartId) => cartItems.FindAll(item => item.CartId == cartId));

            // Метод для добавления нового элемента в корзину
            mockCartItem.Setup(repo => repo.AddCartItemAsync(It.IsAny<CartItem>()))
                .Returns(Task.CompletedTask);

            // Метод для обновления элемента в корзине
            mockCartItem.Setup(repo => repo.UpdateCartItemAsync(It.IsAny<CartItem>()))
                .Returns(Task.CompletedTask);

            // Метод для удаления элемента корзины по ID
            mockCartItem.Setup(repo => repo.DeleteCartItemAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Метод для удаления всех элементов из корзины по cartId
            mockCartItem.Setup(repo => repo.DeleteCartItemsByCartIdAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            return mockCartItem;
        }
    }
}