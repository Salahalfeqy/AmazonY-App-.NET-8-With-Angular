using AmazonY.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired();
            builder.HasData(
                new Category { id=1, Name= "test", Description="test" }
                );
        }
    }
}
