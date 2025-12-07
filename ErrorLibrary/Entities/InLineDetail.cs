namespace ErrorLibrary.Entities
{
    public class InLineDetail
    {
        public int Id { get; set; }
        public int InLineId { get; set; }
        public int TimeFrameId { get; set; }
        public int ErrorId { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int Quantity { get; set; }

        public Error Error { get; set; }
        public InLine InLine { get; set; }
        public TimeFrame TimeFrame { get; set; }
    }
}
