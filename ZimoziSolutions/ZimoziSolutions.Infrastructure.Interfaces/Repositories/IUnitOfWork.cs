
namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();

        Task Rollback();
    }
}
