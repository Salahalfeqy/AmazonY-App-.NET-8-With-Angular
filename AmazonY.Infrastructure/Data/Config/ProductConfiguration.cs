
using AmazonY.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Infrastructure.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired();
                   

            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product { id = 1, Name = "test ", Description = "test", CategoryId = 1, Price = 123 }
                );
        }
    }
}
