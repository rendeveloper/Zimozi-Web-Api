using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _repository;

        public UserRepository(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public IQueryable<User> Users => _repository.Entities;

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _repository.Entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _repository.Entities.FirstOrDefaultAsync(e => e.Username == name);
        }

        public async Task AddAsync(User model)
        {
            await _repository.AddAsync(model);
        }

        public void Update(User model)
        {
            _repository.Update(model);
        }

        public void Delete(User model)
        {
            _repository.Delete(model);
        }
    }
}
