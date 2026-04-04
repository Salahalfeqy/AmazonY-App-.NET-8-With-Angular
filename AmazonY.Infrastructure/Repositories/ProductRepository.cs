using AmazonY.Core.DTO;
using AmazonY.Core.Entities.Product;
using AmazonY.Core.Interfaces;
using AmazonY.Core.Services;
using AmazonY.Core.Sharing;
using AmazonY.Infrastructure.Data;
using AmazonY.Infrastructure.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<ProductDTO>>GetAllAsync(ProductParams productParams)
        {
            var query = context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();
            //filtering by word
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                //query = query.Where(
                //    m => m.Name.ToLower().Contains(productParams.Search.ToLower())
                //    ||
                //m.Description.ToLower().Contains( productParams.Search.ToLower() )
                //);

                var searchWords = productParams.Search.Split(' ');
                query = query.Where(m=> searchWords.All(
                    word=> 
                    m.Name.ToLower().Contains(word.ToLower())
                    ||m.Description.ToLower().Contains(word.ToLower()) 
                    ));


            }




            //filtering by category id logic
            if (productParams.CategoryId.HasValue)
                query = query.Where(m => m.CategoryId == productParams.CategoryId);

            //sorting logic
            if (!string.IsNullOrEmpty(productParams.sort))
            {
                query = productParams.sort switch
                {
                    "PriceAce" => query.OrderBy(m => m.NewPrice),
                    "PriceDce" => query.OrderByDescending(m => m.NewPrice),
                    _ => query.OrderBy(m => m.Name),
                };
            }
            //pagination logic here
            // pagination logic should alwaiys be the last logic in the function

            //this logic is added to product params class so we commented it here
            //productParams.pageNumber = productParams.pageNumber > 0 ? productParams.pageNumber : 1;
            //productParams.pageSize = productParams.pageSize > 0 ? productParams.pageSize : 3;


             query = query.Skip((productParams.pageSize) *(productParams.pageNumber - 1)).Take(productParams.pageSize);
            var result = mapper.Map<List<ProductDTO>>(query);
            return result;
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
