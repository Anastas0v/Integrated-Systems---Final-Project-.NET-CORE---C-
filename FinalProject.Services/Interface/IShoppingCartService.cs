using FinalProject.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
