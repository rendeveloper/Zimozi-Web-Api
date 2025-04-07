using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Infrastructure.Interfaces.Repositories
{
    public interface INotificationsRepository
    {
        IQueryable<Notifications> Notifications { get; }
        Task<Notifications> GetByIdAsync(int id);
        Task<IQueryable<Notifications>> GetAllAsync();
        Task AddAsync(Notifications model);
        void Update(Notifications model);
        void Delete(Notifications model);
    }
}
