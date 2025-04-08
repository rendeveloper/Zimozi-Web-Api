
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Domain.UserTask;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Infrastructure.Repositories
{
    public class UserTasksRepository : IUserTasksRepository
    {
        private readonly IGenericRepository<UserTasks> _repository;

        public UserTasksRepository(IGenericRepository<UserTasks> repository)
        {
            _repository = repository;
        }

        public IQueryable<UserTasks> UserTasks => _repository.Entities;

        public async Task<IQueryable<OTask>> GetUserTasks(User user)
        {
            List<OTask> list = await _repository.Entities.Where(u => u.UserId == user.Id)
                                .Select(c => new OTask
                                {
                                    Id = c.TaskId,
                                    Description = c.Task.Description,
                                    Status = c.Task.Status,
                                    DueDate = c.Task.DueDate,
                                    AssignedUserId = user.Id,
                                    TaskCommentsId = c.Task.TaskCommentsId,
                                    NotificationsId = c.Task.NotificationsId
                                }).ToListAsync();
            return list.AsQueryable();
        }

        public async Task AddAsync(UserTasks userTasks)
        {
            await _repository.AddAsync(userTasks);
        }

        public void Update(UserTasks model)
        {
            _repository.Update(model);
        }

        public void Delete(UserTasks model)
        {
            _repository.Delete(model);
        }
    }
}
