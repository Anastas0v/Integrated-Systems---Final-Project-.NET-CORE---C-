using FinalProject.Domain.DomainModels;
using FinalProject.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalProject.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ticket> Bileti { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd(); //adds new id values

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            //builder.Entity<ProductInShoppingCart>()
            //    .HasKey(z => new { z.BiletId, z.ShoppingCartId }); //many 2 many relation KEYS

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.Bilet)
                .WithMany(z => z.ProductInShoppingCarts)  //one ticket many shoppng carts
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.ProductInShoppingCarts)  //one ticket many shoppng carts
                .HasForeignKey(z => z.BiletId);

            builder.Entity<ShoppingCart>()
                .HasOne<ApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId); //one2one relation

            //builder.Entity<ProductInOrder>()
            //    .HasKey(z => new { z.TicketId, z.OrderId });

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.SelectedTicket)
                .WithMany(z => z.Orders)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.Tickets)
                .HasForeignKey(z => z.OrderId);
        }

        public DbSet<FinalProject.Domain.DomainModels.Bileti> Bileti_1 { get; set; }
    }
}
