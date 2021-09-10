using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Domain.DomainModels
{
    public class ProductInShoppingCart : BaseEntity
    {
        public Guid BiletId { get; set; }
        public Ticket Bilet { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
