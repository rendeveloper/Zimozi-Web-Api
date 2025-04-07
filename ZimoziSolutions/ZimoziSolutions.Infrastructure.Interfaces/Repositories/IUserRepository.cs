using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByNameAsync(string name);
        Task AddAsync(User model);
        void Update(User model);
        void Delete(User model);
    }
}
