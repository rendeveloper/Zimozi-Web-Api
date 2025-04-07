
using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        IQueryable<OTask> Tasks { get; }
        Task<OTask> GetByEmailAsync(string email);
        Task<List<OTask>> GetListAsync();
        Task<IQueryable<OTask>> GetAllAsync();
        Task<IQueryable<OTask>> GetListFilteredByActive(bool activeTask);
        Task AddAsync(OTask model);
        void Update(OTask model);
    }
}
