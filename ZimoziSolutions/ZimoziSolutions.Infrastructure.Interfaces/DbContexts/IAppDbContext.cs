using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Infrastructure.Interfaces.DbContexts
{
    public interface IAppDbContext
    {
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        DbSet<OTask> Tasks { get; set; }
    }
}
