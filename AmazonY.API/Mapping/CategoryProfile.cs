using AmazonY.Core.DTO;
using AmazonY.Core.Entities.Product;
using AutoMapper;

namespace AmazonY.API.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO,Category>().ReverseMap();
        }
    }
}
