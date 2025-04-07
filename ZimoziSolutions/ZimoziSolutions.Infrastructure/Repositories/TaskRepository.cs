
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;
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

        public async Task<OTask> GetByIdAsync(int id)
        {
            return await _repository.Entities
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IQueryable<OTask>> GetListFilteredByAssignedUserId(int userId)
        {
            List<OTask> list = await _repository.Entities.Where(e => e.AssignedUserId == userId).ToListAsync();

            return list.AsQueryable();
        }

        public async Task<IQueryable<OTask>> GetByStatusAsync(string status)
        {
            List<OTask> list = await _repository.Entities.Where(e => e.Status == status).ToListAsync();
            return list.AsQueryable();
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

        public async Task AddAsync(OTask task)
        {
            await _repository.AddAsync(task);
        }

        public void Update(OTask task)
        {
            _repository.Update(task);
        }

        public void Delete(OTask task)
        {
            _repository.Delete(task);
        }
    }
}
