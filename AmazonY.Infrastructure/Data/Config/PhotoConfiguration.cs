using AmazonY.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Infrastructure.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Photo> builder)
        {
           builder.HasData(new Photo
                {
                    id = 1,
                    ImageName = "https://m.media-amazon.com/images/I/71w+q9Zt2sL._AC_SX679_.jpg",
                    ProductId = 1
                }
            );
        }
    }
}
