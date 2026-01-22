using ErrorLibrary.Helper.Enums;

namespace ErrorLibrary.Entities
{
    public class InspectionRound
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public Result Result { get; set; }

        public Inspection Inspection { get; set; }
    }
}
