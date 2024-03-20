using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
