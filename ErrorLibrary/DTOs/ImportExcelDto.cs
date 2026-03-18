namespace ErrorLibrary.DTOs
{
    public class ImportExcelDto
    {
        public IFormFile File { get; set; }
        public int WorksheetIndex { get; set; } = 0;
    }
}
