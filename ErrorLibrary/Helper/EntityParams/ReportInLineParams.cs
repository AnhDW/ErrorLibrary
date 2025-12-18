namespace ErrorLibrary.Helper.EntityParams
{
    public class ReportInLineParams : PaginationParams
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? RowTake { get; set; }
    }
}
