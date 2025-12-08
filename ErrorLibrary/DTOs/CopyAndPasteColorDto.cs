namespace ErrorLibrary.DTOs
{
    public class CopyAndPasteColorDto
    {
        public int TimeFrameId { get; set; }
        public List<int> TimeFrameColorIds { get; set; } = new List<int>();
    }
}
