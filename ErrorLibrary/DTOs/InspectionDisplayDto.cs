using ErrorLibrary.Entities;
using ErrorLibrary.Helper.Enums;

namespace ErrorLibrary.DTOs
{
    public class InspectionDisplayDto
    {
        public int Id { get; set; }
        public int ReportFinalFactoryDetailId { get; set; }
        public InspectionType InspectionType { get; set; }
        public int Major { get; set; } = 0;
        public int Minor { get; set; } = 0;

        public List<InspectionRoundDto> InspectionRounds { get; set; } = new();
    }
}