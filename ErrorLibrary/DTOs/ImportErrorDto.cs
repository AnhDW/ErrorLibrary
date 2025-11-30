namespace ErrorLibrary.DTOs
{
    public class ImportErrorDto
    {
        public IFormFile File { get; set; }
        public int WorksheetIndex { get; set; } = 0;
    }
}
