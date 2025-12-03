namespace ErrorLibrary.Entities
{
    public class TimeFrame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<TimeFrameColor> TimeFrameColors { get; set; } = new();
        public List<InLineDetail> InLineDetails { get; set; } = new();
    }
}
