using System;
using System.Collections.Generic;
using System.Text;
using CashBack.Data.Configuration;
using CashBack.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.Context
{
    public class CashBackContext : DbContext
    {
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public CashBackContext(DbContextOptions<CashBackContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RetailerConfiguration());
            builder.ApplyConfiguration(new PurchaseConfiguration());
        }

    }
}
