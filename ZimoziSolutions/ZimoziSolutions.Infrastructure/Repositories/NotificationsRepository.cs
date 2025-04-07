
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Infrastructure.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IGenericRepository<Notifications> _repository;

        public NotificationsRepository(IGenericRepository<Notifications> repository)
        {
            _repository = repository;
        }

        public IQueryable<Notifications> Notifications => _repository.Entities;

        public async Task<Notifications> GetByIdAsync(int id)
        {
            return await _repository.Entities
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IQueryable<Notifications>> GetAllAsync()
        {
            List<Notifications> list = await _repository.Entities
                .Include(x => x.Tasks)
                .ToListAsync();

            return list.AsQueryable();
        }

        public async Task AddAsync(Notifications model)
        {
            await _repository.AddAsync(model);
        }

        public void Update(Notifications model)
        {
            _repository.Update(model);
        }

        public void Delete(Notifications model)
        {
            _repository.Delete(model);
        }
    }
}
