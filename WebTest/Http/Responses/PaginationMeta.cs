namespace WebTest.Http.Responses
{
    public class PaginationMeta
    {
        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
            
        public int Total { get; set; }
    }
}
