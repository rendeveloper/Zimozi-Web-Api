
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IGenericRepository<OTask> _repository;

        public TaskRepository(IGenericRepository<OTask> repository)
        {
            _repository = repository;
        }

        public IQueryable<OTask> Tasks => _repository.Entities;

        public async Task<OTask> GetByEmailAsync(string email)
        {
            return await _repository.Entities.FirstOrDefaultAsync(e => e.Id == 0);
        }

        public async Task<List<OTask>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<IQueryable<OTask>> GetAllAsync()
        {
            List<OTask> list = await _repository.Entities.ToListAsync();

            return list.AsQueryable();
        }

        public async Task<IQueryable<OTask>> GetListFilteredByActive(bool activeTasks)
        {
            List<OTask> list = await _repository.Entities.Where(e => e.Id == 0).ToListAsync();

            return list.AsQueryable();
        }

        public async Task AddAsync(OTask task)
        {
            await _repository.AddAsync(task);
        }

        public void Update(OTask task)
        {
            _repository.Update(task);
        }
    }
}
