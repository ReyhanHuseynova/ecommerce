using ecommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ecommerce.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) 
        {
                
        }

        public DbSet<Slider> Sliders  { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Contact> Contacts  { get; set; }
        public DbSet<Stock> Stocks  { get; set; }
        public DbSet<Cart> Carts  { get; set; }
        public DbSet<Order> Orders  { get; set; }
    
    }
}
