namespace ErrorLibrary.DTOs
{
    public class InLineDetailDto
    {
        public int Id { get; set; }
        public int InLineId { get; set; }
        public int TimeFrameColorId { get; set; }
        public int ErrorId { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int Quantity { get; set; }
    }
}
