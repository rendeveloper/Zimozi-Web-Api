
using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        IQueryable<OTask> Tasks { get; }
        Task<OTask> GetByIdAsync(int id);
        Task<IQueryable<OTask>> GetListFilteredByAssignedUserId(int userId);
        Task<IQueryable<OTask>> GetByStatusAsync(string status);
        Task<List<OTask>> GetListAsync();
        Task<IQueryable<OTask>> GetAllAsync();
        Task AddAsync(OTask model);
        void Update(OTask model);
        void Delete(OTask model);
    }
}
