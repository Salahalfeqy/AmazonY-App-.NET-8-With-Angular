using AmazonY.Core.DTO;
using AmazonY.Core.Entities.Product;
using AmazonY.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // 
        Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);
        Task DeleteAsync(Product product);

    }
}
