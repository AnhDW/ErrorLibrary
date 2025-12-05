namespace ErrorLibrary.Entities
{
    public class TimeFrame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public List<TimeFrameColor> TimeFrameColors { get; set; } = new();
        public List<InLineDetail> InLineDetails { get; set; } = new();
    }
}
