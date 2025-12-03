namespace ErrorLibrary.Entities
{
    public class InLineDetail
    {
        public int Id { get; set; }
        public int InLineId { get; set; }
        public int TimeFrameColorId { get; set; }
        public int ErrorId { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int Quantity { get; set; }

        public Error Error { get; set; }
        public InLine InLine { get; set; }
        public TimeFrameColor TimeFrameColor { get; set; }
    }
}
