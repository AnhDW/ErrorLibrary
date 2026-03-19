namespace ErrorLibrary.DTOs
{
    public class PreviewReportFinalFactoryDetailExcelDto
    {
        public List<CustomerDto> Customers { get; set; } = new();
        public List<StyleDto> Styles { get; set; } = new();
        public List<string> CustomerCodesExcept { get; set; } = new();
        public List<string> StyleCodesExcept { get; set; } = new();
        public List<ReportFinalFactoryDetailGridDto> Excel { get; set; } = new();
    }
}
