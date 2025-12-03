namespace ErrorLibrary.Entities
{
    public class TimeRange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<TimeRangeColorByQuantity> TimeRangeColorByQuantities { get; set; } = new();
    }
}
