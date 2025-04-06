
namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);

        void Update(T entity);
    }
}
