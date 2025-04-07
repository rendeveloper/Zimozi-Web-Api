using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<User> GetByIdAsync(int id);
        Task<User> GetByGuidAsync(Guid id);
        Task<User> GetByNameAsync(string name);
        Task<IQueryable<User>> GetListFilteredByRole(string role);
        Task<List<User>> GetListAsync();
        Task<IQueryable<User>> GetAllAsync();
        Task AddAsync(User model);
        void Update(User model);
        void Delete(User model);
    }
}
