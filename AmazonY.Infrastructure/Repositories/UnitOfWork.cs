using AmazonY.Core.Interfaces;
using AmazonY.Core.Services;
using AmazonY.Infrastructure.Data;
using AutoMapper;
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
        private readonly IImageManagementService _imageManagementService;
        private readonly IMapper _mapper;

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public IProductRepository ProductRepository { get; }
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        {
            _Context = context; _mapper = mapper;
            _imageManagementService = imageManagementService;
            CategoryRepository = new CategoryRepository(_Context);
            PhotoRepository = new PhotoRepository(_Context);
            ProductRepository = new ProductRepository(_Context, _mapper, _imageManagementService);
           
        }
    }
}
