using Ardalis.Specification;

namespace Application.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
