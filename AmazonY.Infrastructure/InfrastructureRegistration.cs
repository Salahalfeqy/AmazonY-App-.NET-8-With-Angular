using AmazonY.Core.Interfaces;
using AmazonY.Core.Services;
using AmazonY.Infrastructure.Data;
using AmazonY.Infrastructure.Repositories;
using AmazonY.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AmazonY.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services,IConfiguration Configuration ) 
        {
            //services.AddTransient
            //services.AddScoped
            //services.AddSingleton
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            // INSTEAD we can apply IUnitOfWork like this :
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IImageManagementService, ImageManagementService>();
        //    Services.AddSingleton<IFileProvider>(
        //new PhysicalFileProvider(
        //    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
        //));
            // apply DbContext
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<AppDbContext>(
                op => op.UseSqlServer(Configuration.GetConnectionString("AmazonYDataBase"))
            );

            return services;
        }
    }
}
