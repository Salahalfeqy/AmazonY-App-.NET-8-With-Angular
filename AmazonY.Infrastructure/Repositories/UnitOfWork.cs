using AmazonY.Core.Interfaces;
using AmazonY.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context ;
        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public IProductRepository ProductRepository { get; }
        public UnitOfWork(AppDbContext context)
        {
            _Context = context;
            CategoryRepository = new CategoryRepository(_Context);
            PhotoRepository = new PhotoRepository(_Context);
            ProductRepository = new ProductRepository(_Context);
        }
    }
}
