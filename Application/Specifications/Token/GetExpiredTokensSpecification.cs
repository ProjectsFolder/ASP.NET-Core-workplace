using Ardalis.Specification;

namespace Application.Specifications.Token;

internal class GetExpiredTokensSpecification : Specification<Domain.Token>
{
    public GetExpiredTokensSpecification(int seconds)
    {
        var time = DateTime.UtcNow.AddSeconds(-seconds);
        Query.Where(t => t.CreatedAt < time);
    }
}
