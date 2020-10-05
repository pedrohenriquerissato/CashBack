using System;
using System.Collections.Generic;
using System.Text;
using CashBack.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBack.Data.Configuration
{
    class RetailerConfiguration : IEntityTypeConfiguration<Retailer>
    {
        public void Configure(EntityTypeBuilder<Retailer> builder)
        {
            builder.ToTable("Retailer");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn();
            builder.HasIndex(r => r.DocumentId).IsUnique();
            builder.Property(r => r.DocumentId).IsRequired().HasMaxLength(15);
            builder.Property(r => r.FullName).IsRequired().HasMaxLength(100);
            builder.HasIndex(r => r.Email).IsUnique();
            builder.Property(r => r.Email).IsRequired();
            builder.Property(r => r.Password).IsRequired();
        }
    }
}
