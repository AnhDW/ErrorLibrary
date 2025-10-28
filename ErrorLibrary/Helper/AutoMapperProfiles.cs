using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using System.Diagnostics.Metrics;

namespace ErrorLibrary.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Error, ErrorDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ErrorGroup, ErrorGroupDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<Solution, SolutionDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Unit, UnitDto>().ReverseMap();
            CreateMap<Factory, FactoryDto>().ReverseMap();
            CreateMap<Enterprise, EnterpriseDto>().ReverseMap();
        }
    }
}
