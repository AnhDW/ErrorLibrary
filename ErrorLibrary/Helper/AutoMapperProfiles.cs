using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;

namespace ErrorLibrary.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Error, ErrorDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ErrorGroup, ErrorCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<Solution, SolutionDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

        }
    }
}
