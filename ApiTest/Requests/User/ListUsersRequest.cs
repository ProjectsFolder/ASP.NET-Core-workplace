using Application.Common.Mappings;
using Application.Domains.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Requests.User;

public class ListUsersRequest : BaseMappingTo<GetUsersPaginatedQuery>
{
    [FromQuery]
    public int PerPage { get; init; }

    [FromQuery]
    public int Page { get; init; }
}
