using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Domain.DomainModels
{
    public class Bileti
    {
        public Guid Id { get; set; }
        [Required]
        public string ImeNaFilm { get; set; }
        [Required]
        public string Reziser { get; set; }
        [Required]
        public string Zanr { get; set; }
        [Required]
        public int DatumNaIzdavanje { get; set; }
        [Required]
        public int Validnost { get; set; }
        [Required]
        public int Cena { get; set; }

        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual ICollection<ProductInOrder> Orders { get; set; }
    }
}
