using AmazonY.Core.DTO;
using AmazonY.Core.Entities.Product;
using AutoMapper;

namespace AmazonY.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product , ProductDTO>()
                .ForMember(
                x=> x.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(x => x.Photos, op =>op.Ignore())
                .ReverseMap();
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(x => x.Photos, op =>op.Ignore())
                .ReverseMap();
        }
    }
}
