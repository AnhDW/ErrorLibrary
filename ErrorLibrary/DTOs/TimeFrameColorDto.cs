namespace ErrorLibrary.DTOs
{
    public class TimeFrameColorDto
    {
        public int Id { get; set; }
        public int TimeFrameId { get; set; }
        public string HexCode { get; set; } = "#3EE0CD";
        public int MinQuantity { get; set; } = 0;
        public int MaxQuantity { get; set; } = 0;
    }
}
