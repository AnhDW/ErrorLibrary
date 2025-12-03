namespace ErrorLibrary.Entities
{
    public class TimeRangeColorByQuantity
    {
        public int TimeRangeId { get; set; }
        public string HexCode { get; set; } = "#3EE0CD";
        public int MinQuantity { get; set; } = 0;
        public int MaxQuantity { get; set; } = 0;

        public TimeRange TimeRange { get; set; }
    }
}
