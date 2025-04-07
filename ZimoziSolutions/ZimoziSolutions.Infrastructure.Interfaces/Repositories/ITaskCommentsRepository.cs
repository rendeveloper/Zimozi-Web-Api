
using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface ITaskCommentsRepository
    {
        IQueryable<TaskComments> TaskComments { get; }
        Task<TaskComments> GetByIdAsync(int id);
        Task<IQueryable<TaskComments>> GetAllAsync();
        Task AddAsync(TaskComments model);
        void Update(TaskComments model);
        void Delete(TaskComments model);
    }
}
