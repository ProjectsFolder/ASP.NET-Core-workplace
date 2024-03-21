using Ardalis.Specification;

namespace Application.Specifications.Token;

public class TokensByUserSpecification : Specification<Domain.Token>
{
    public TokensByUserSpecification(int userId) : base()
    {
        Query.Where(t => t.UserId == userId);
    }
}
