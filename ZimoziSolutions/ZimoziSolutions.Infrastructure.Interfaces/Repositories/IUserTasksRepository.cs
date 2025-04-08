
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Domain.UserTask;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface IUserTasksRepository
    {
        IQueryable<UserTasks> UserTasks { get; }
        Task<IQueryable<OTask>> GetUserTasks(User user);
        Task AddAsync(UserTasks userTasks);
        void Update(UserTasks model);
        void Delete(UserTasks model);
    }
}
