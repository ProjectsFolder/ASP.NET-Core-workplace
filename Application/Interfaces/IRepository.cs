using Ardalis.Specification;

namespace Application.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
