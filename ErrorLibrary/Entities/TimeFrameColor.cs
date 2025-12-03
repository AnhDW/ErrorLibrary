namespace ErrorLibrary.Entities
{
    public class TimeFrameColor
    {
        public int Id { get; set; }
        public int TimeFrameId { get; set; }
        public string HexCode { get; set; } = "#3EE0CD";
        public int MinQuantity { get; set; } = 0;
        public int MaxQuantity { get; set; } = 0;

        public TimeFrame TimeFrame { get; set; }
        public List<InLineDetail> InLineDetails { get; set; } = new();
    }
}
