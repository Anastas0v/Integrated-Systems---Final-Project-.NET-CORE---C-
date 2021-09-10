using FinalProject.Domain.DomainModels;
using FinalProject.Domain.DTO;
using FinalProject.Repository.Interface;
using FinalProject.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepositorty;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<EmailMessage> _emailRepository;

        public ShoppingCartService(IRepository<EmailMessage> emailRepository,IRepository<ShoppingCart> shoppingCartRepository, IRepository<ProductInOrder> productInOrderRepositorty, IRepository<Order> orderRepositorty, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepositorty;
            _productInOrderRepositorty = productInOrderRepositorty;
            _emailRepository = emailRepository;
        }

        public bool deleteProductFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.Bilet.Id.Equals(id)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);
    
                return true;
            }

            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var AllProducts = userShoppingCart.ProductInShoppingCarts.ToList();

            var allProductPrice = AllProducts.Select(z => new
            {
                ProductPrice = z.Bilet.Price,
                Quanitity = z.Quantity
            }).ToList();

            double totalPrice = 0;


            foreach (var item in allProductPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            ShoppingCartDto scDto = new ShoppingCartDto
            {
                ProductInShoppingCarts = AllProducts,
                TotalPrice = totalPrice
            };


            return scDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);
                var userShoppingCart = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Order Created";
                message.Status = false;


                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);
                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    TicketId = z.Bilet.Id,
                    OrderId = order.Id,
                    UserOrder = order,
                    SelectedTicket = z.Bilet,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Your Order Is Completed. The Order Contains: ");
                var totalPrice = 0.0;

                for(int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Quantity * item.SelectedTicket.Price;
                    sb.AppendLine("Ticket No. " + i.ToString() + ": " + item.SelectedTicket.Name + " Price: " + item.SelectedTicket.Price + "€ & Quantity: " + item.Quantity);
                }

                sb.AppendLine("Total Price: " + totalPrice.ToString());
                message.Content = sb.ToString();

                productInOrders.AddRange(result);

                foreach(var item in productInOrders)
                {
                    this._productInOrderRepositorty.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                this._emailRepository.Insert(message);
                this._userRepository.Update(loggedInUser);
                return true;
            }

            return false;
        }
    }
}
