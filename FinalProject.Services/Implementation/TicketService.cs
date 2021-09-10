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
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {

            var user = this._userRepository.Get(userID);
            var userShoppingCart = user.UserCart;

            if (item.TicketId != null && userShoppingCart != null)
            {
                var product = this.GetDetailsForProduct(item.TicketId);
                if (product != null)
                {
                    ProductInShoppingCart itemToAdd = new ProductInShoppingCart
                    {
                        Bilet = product,
                        BiletId = product.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };
                    this._productInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewProduct(Ticket p)
        {
            this._ticketRepository.Insert(p);
        }

        public void DeleteProduct(Guid id)
        {
            var ticket = this.GetDetailsForProduct(id);
            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllProducts()
        {
            return this._ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForProduct(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForProduct(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };
            return model;
        }

        public void UpdateExistingProduct(Ticket p)
        {
            this._ticketRepository.Update(p);
        }
    }
}
