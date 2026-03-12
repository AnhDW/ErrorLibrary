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
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();
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

            CreateMap<EndLine, EndLineDto>().ReverseMap();
            CreateMap<EndLine, EndLineDisplayDto>().ReverseMap();
            CreateMap<EndLine, InitAndUpdateEndLineDto>().ReverseMap();
            CreateMap<EndLineDetail, EndLineDetailDto>().ReverseMap();
            CreateMap<EndLineDetail, EndLineDetailDisplayDto>().ReverseMap();

            CreateMap<Style, StyleDto>().ReverseMap();
            CreateMap<Defect, DefectDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<ReportFinalFactory, ReportFinalFactoryDto>().ReverseMap();
            CreateMap<ReportFinalFactoryDetail, ReportFinalFactoryDetailDto>().ReverseMap();
            CreateMap<ReportFinalFactoryDetail, ReportFinalFactoryDetailDisplayDto>().ReverseMap();
            CreateMap<ReportFinalFactoryDetailDefect, ReportFinalFactoryDetailDefectDto>().ReverseMap();

            CreateMap<ReportFinalFactoryDetail, ReportFinalFactoryDetailGridDto>()
                .ForMember(dest => dest.PreFinalResult1, opt => opt.NullSubstitute(0))
                .ForMember(dest => dest.PreFinalResult2, opt => opt.NullSubstitute(0))
                .ForMember(dest => dest.PreFinalResult3, opt => opt.NullSubstitute(0))
                .ForMember(dest => dest.FinalResult1, opt => opt.NullSubstitute(0))
                .ForMember(dest => dest.FinalResult2, opt => opt.NullSubstitute(0))
                .ForMember(dest => dest.FinalResult3, opt => opt.NullSubstitute(0))
                .ForMember(dest => dest.PreFinalDate1, opt => opt.NullSubstitute(DateTime.MinValue))
                .ForMember(dest => dest.PreFinalDate2, opt => opt.NullSubstitute(DateTime.MinValue))
                .ForMember(dest => dest.PreFinalDate3, opt => opt.NullSubstitute(DateTime.MinValue))
                .ForMember(dest => dest.FinalDate1, opt => opt.NullSubstitute(DateTime.MinValue))
                .ForMember(dest => dest.FinalDate2, opt => opt.NullSubstitute(DateTime.MinValue))
                .ForMember(dest => dest.FinalDate3, opt => opt.NullSubstitute(DateTime.MinValue));
        }
    }
}
