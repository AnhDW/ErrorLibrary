using ErrorLibrary.Helper.Enums;

namespace ErrorLibrary.DTOs
{
    public class InspectionDto
    {
        public int Id { get; set; }
        public int ReportFinalFactoryDetailId { get; set; }
        public InspectionType InspectionType { get; set; }
        public int Major { get; set; } = 0;
        public int Minor { get; set; } = 0;
    }
}
