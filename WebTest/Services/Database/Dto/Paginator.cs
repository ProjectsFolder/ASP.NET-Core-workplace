namespace WebTest.Services.Database.Dto
{
    public class Paginator<TModel>
    {
        public IEnumerable<TModel> Data { get; set; } = Enumerable.Empty<TModel>();

        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int Total { get; set; }
    }
}
