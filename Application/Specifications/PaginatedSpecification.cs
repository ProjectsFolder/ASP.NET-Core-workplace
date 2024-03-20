using Ardalis.Specification;

namespace Application.Specifications;

public class PaginatedSpecification<T> : Specification<T>
{
    public PaginatedSpecification(int page, int pageSize, int total, out int totalPages) : base()
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 1;
        }

        totalPages = (int)Math.Ceiling((float)total / pageSize);
        if (page > totalPages)
        {
            page = totalPages;
        }

        Query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
