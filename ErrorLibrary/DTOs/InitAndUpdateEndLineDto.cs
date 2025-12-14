namespace ErrorLibrary.DTOs
{
    public class InitAndUpdateEndLineDto
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; } = 0;
        public int CheckQuantity { get; set; } = 0;
        public int AcceptedQuantity { get; set; } = 0;
        public bool IsActive { get; set; }
        public bool IsFinalized { get; set; }

        public bool FirstLoad { get; set; }
    }
}
