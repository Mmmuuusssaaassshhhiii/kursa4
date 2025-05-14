using kursa4.Interfaces;
using kursa4.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kursa4.Mocks
{
    public class MockCart
    {
        public static Mock<ICart> GetMockCart()
        {
            var mockCart = new Mock<ICart>();

            var carts = new List<Cart>
            {
                new Cart
                {
                    Id = 1,
                    UserId = 1,
                    CartItems = new List<CartItem>
                    {
                        new CartItem { Id = 1, CartId = 1, LaptopId = 1, Quantity = 2 },
                        new CartItem { Id = 2, CartId = 1, LaptopId = 2, Quantity = 1 }
                    }
                }
            };

            var cartItems = new List<CartItem>
            {
                new CartItem { Id = 1, CartId = 1, LaptopId = 1, Quantity = 2 },
                new CartItem { Id = 2, CartId = 1, LaptopId = 2, Quantity = 1 }
            };

            // Настройка методов mock-объекта

            // Метод для получения корзины пользователя по его UserId
            mockCart.Setup(repo => repo.GetCartByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) => carts.Find(cart => cart.UserId == userId));

            // Метод для добавления новой корзины
            mockCart.Setup(repo => repo.AddCartAsync(It.IsAny<Cart>()))
                .Returns(Task.CompletedTask);

            // Метод для обновления корзины
            mockCart.Setup(repo => repo.UpdateCartAsync(It.IsAny<Cart>()))
                .Returns(Task.CompletedTask);

            // Метод для удаления корзины
            mockCart.Setup(repo => repo.DeleteCartAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Метод для добавления товара в корзину
            mockCart.Setup(repo => repo.AddCartItemAsync(It.IsAny<int>(), It.IsAny<CartItem>()))
                .Returns(Task.CompletedTask);

            // Метод для удаления товара из корзины
            mockCart.Setup(repo => repo.RemoveCartItemAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Метод для получения всех товаров из корзины
            mockCart.Setup(repo => repo.GetCartItemsAsync(It.IsAny<int>()))
                .ReturnsAsync((int cartId) => cartItems.FindAll(item => item.CartId == cartId));

            return mockCart;
        }
    }
}
