using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        //ctor to create Constructor
        public AppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Models.Stocks> Stocks { get; set; }
        public DbSet<Models.Comment> comments{get; set;}

        public DbSet<Portfolio> portfolios{get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p=> new {p.AppUserId,p.StockId}));

            builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.portfolios)
            .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
            .HasOne(u => u.Stocks)
            .WithMany(u => u.portfolios)
            .HasForeignKey(p => p.StockId);
            


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },

                new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            
        }
    }
}