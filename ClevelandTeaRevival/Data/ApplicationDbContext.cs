using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClevelandTeaRevival.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Administrator> Admin { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Tea> Teas { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionTab> TransactionTabs { get; set; }

    }
}
