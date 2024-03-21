namespace Application.Common.Dto;

public class PaginationDto<T>
{
    public required IEnumerable<T> Items { get; set; }

    public required int TotalPages { get; set; }
}
