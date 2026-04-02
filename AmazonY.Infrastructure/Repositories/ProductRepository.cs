using AmazonY.Core.DTO;
using AmazonY.Core.Entities.Product;
using AmazonY.Core.Interfaces;
using AmazonY.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AmazonY.Core.Services;
using Microsoft.EntityFrameworkCore;
using AmazonY.Infrastructure.Service;

namespace AmazonY.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        private readonly IImageManagementService imageManagementService;

        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.mapper = mapper;
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;
            var product = mapper.Map<Product>(productDTO);

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImagePath =await imageManagementService.AddImageAsync(productDTO.Photo , productDTO.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.id,
            }).ToList();

            await context.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return  true;
        }


        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }
            var FindProduct = await context.Products.Include(m => m.Category).Include(m => m.Photos).FirstOrDefaultAsync(m => m.id == updateProductDTO.id);

            if (FindProduct is null) { return false; }

            mapper.Map(updateProductDTO , FindProduct);

            var FindPhoto = await context.Photos.Where(x => x.ProductId == updateProductDTO.id).ToListAsync();
            foreach (var item in FindPhoto)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
                context.Photos.RemoveRange(FindPhoto);

                var ImagePath = await imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
                var photo = ImagePath.Select(path => new Photo
                {
                    ImageName = path,
                    ProductId = updateProductDTO.id,
                }
                    ).ToList();

                await context.Photos.AddRangeAsync(photo);
                await context.SaveChangesAsync();
            }
            return true;

            

        }
        public async Task DeleteAsync(Product product)
        {
           var photo =  await context.Photos.Where(x => x.ProductId== product.id).ToListAsync();

            foreach(var item in photo)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
