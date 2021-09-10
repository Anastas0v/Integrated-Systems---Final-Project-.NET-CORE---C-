using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"[2-3]D",ErrorMessage = "2D/3D")]
        [StringLength(2)]
        public string Format { get; set; }
        [Required]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$",ErrorMessage ="Invalid Date Format. DD/MM/YYYY")]
        public string Date { get; set; }
        [Required]
        [RegularExpression(@"^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$",ErrorMessage = "Invalid Time Format. HH:MM IN 24Format")]
        public string Time { get; set; }
        [Required]
        [Range(1,20,ErrorMessage ="Rooms Are From 1 - 20")]
        public int Room { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Rooms Are From 1 - 20")]
        public int Row { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Rooms Are From 1 - 20")]
        public int Seat { get; set; }
        [Required]
        [Range (typeof(double), "0.50","25.00",ErrorMessage ="Prices Are From 0.5 - 25.00")]
        public double Price { get; set; }

        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual ICollection<ProductInOrder> Orders { get; set; }
    }
}
