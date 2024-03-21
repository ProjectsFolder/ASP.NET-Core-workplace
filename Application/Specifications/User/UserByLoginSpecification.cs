using Ardalis.Specification;

namespace Application.Specifications.User;

public class UserByLoginSpecification : Specification<Domain.User>
{
    public UserByLoginSpecification(string login) : base()
    {
        Query.Where(u => u.Login.Equals(login));
    }
}
