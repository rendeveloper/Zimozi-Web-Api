
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Infrastructure.Repositories
{
    public class TaskCommentsRepository : ITaskCommentsRepository
    {
        private readonly IGenericRepository<TaskComments> _repository;

        public TaskCommentsRepository(IGenericRepository<TaskComments> repository)
        {
            _repository = repository;
        }

        public IQueryable<TaskComments> TaskComments => _repository.Entities;

        public async Task<TaskComments> GetByIdAsync(int id)
        {
            return await _repository.Entities
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IQueryable<TaskComments>> GetAllAsync()
        {
            List<TaskComments> list = await _repository.Entities
                .Include(x => x.Tasks)
                .ToListAsync();

            return list.AsQueryable();
        }

        public async Task AddAsync(TaskComments model)
        {
            await _repository.AddAsync(model);
        }

        public void Update(TaskComments model)
        {
            _repository.Update(model);
        }

        public void Delete(TaskComments model)
        {
            _repository.Delete(model);
        }
    }
}
