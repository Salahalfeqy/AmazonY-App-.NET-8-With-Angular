using AmazonY.API.Mapping;
using AmazonY.API.Middleware;
using AmazonY.Infrastructure;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using System.IO;
namespace AmazonY.API


{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors
        (
                op =>
            op.AddPolicy
            (
                "CORSPolicy",
         builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200")
            )

        );
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.InfrastructureConfiguration(builder.Configuration);
// chat gpt error solve
    //    builder.Services.AddSingleton<IFileProvider>(
    //new PhysicalFileProvider(
    //    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
    //));
 // chat gpt error solve

            //builder.Services.AddAutoMapper( x => x.AddProfile(new CategoryProfile()));
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //builder.Services.AddAutoMapper(cfg =>
            //{
            //    cfg.AddProfile(new CategoryProfile());
            //    cfg.AddProfile(new ProductMapping());
            //}, AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORSPolicy");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
