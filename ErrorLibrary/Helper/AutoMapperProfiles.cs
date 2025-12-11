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
            CreateMap<Error, ErrorDisplayDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ErrorGroup, ErrorGroupDto>().ReverseMap();
            CreateMap<ErrorCategory, ErrorCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<Solution, SolutionDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Unit, UnitDto>().ReverseMap();
            CreateMap<Factory, FactoryDto>().ReverseMap();
            CreateMap<Enterprise, EnterpriseDto>().ReverseMap();
            CreateMap<Line, LineDto>().ReverseMap();
            CreateMap<ErrorDetail, ErrorDetailDto>().ReverseMap();
            CreateMap<ErrorDetail, ErrorDetailDisplayDto>().ReverseMap();
            CreateMap<ErrorDetailAttachment, ErrorDetailAttachmentDto>().ReverseMap();
            CreateMap<TimeFrame, TimeFrameDto>().ReverseMap();
            CreateMap<TimeFrameColor, TimeFrameColorDto>().ReverseMap();
            CreateMap<InLine, InLineDto>().ReverseMap();
            CreateMap<InLine, InLineDisplayDto>().ReverseMap();
            CreateMap<InLine, InitAndUpdateInLineDto>().ReverseMap();
            CreateMap<InLineDetail, InLineDetailDto>().ReverseMap();
            CreateMap<InLineDetail, InLineDetailDisplayDto>().ReverseMap();

        }
    }
}
