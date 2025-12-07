using ErrorLibrary.Entities;

namespace ErrorLibrary.DTOs
{
    public class InLineDetailDisplayDto
    {
        public int Id { get; set; }
        public int InLineId { get; set; }
        public int TimeFrameId { get; set; }
        public int ErrorId { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int Quantity { get; set; }

        public ErrorDisplayDto Error { get; set; }
        public InLineDto InLine { get; set; }
        public TimeFrameDto TimeFrame { get; set; }
    }
}
