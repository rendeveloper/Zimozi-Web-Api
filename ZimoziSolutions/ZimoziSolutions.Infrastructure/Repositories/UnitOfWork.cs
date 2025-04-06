using ZimoziSolutions.Infrastructure.DbContexts;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;

namespace ZimoziSolutions.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public Task Rollback()
        {
            //todo
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }
    }
}
