using FinalProject.Domain.DomainModels;
using FinalProject.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllProducts();
        Ticket GetDetailsForProduct(Guid? id);
        void CreateNewProduct(Ticket p);
        void UpdateExistingProduct(Ticket p);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteProduct(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
