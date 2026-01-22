using AmazonY.Core.Entities.Product;
using AmazonY.Core.Interfaces;
using AmazonY.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
       

        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
