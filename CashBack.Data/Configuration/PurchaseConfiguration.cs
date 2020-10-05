using System;
using System.Collections.Generic;
using System.Text;
using CashBack.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBack.Data.Configuration
{
    class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Date).IsRequired().HasColumnType("datetime2");
            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Status).IsRequired().HasColumnType("nvarchar(20)");

            builder.Property(p => p.Status).HasConversion(
                PurchaseStatus => PurchaseStatus.Value.ToString(),
                Status => new PurchaseStatus
                {
                    Value = Status.ToString()
                }
            );

            builder
                .HasOne(p => p.Retailer)
                .WithMany(p => p.Purchases)
                .HasForeignKey(r => r.RetailerId);
        }
    }
}
