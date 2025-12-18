namespace ErrorLibrary.Helper.EntityParams
{
    public class ReportEndLineParams : PaginationParams
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
