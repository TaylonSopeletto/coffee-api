using System;
using Microsoft.EntityFrameworkCore;
using CoffeeApiV2.Models;

namespace CoffeeApiV2.Data
{
	public class ApiContext : DbContext
	{
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CoffeeShop> CoffeeShops { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}

