using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Infrastructure.Interfaces.DbContexts
{
    public interface IAppDbContext
    {
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        DbSet<OTask> Tasks { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<TaskComments> TaskComments { get; set; }
        DbSet<Notifications> Notifications { get; set; }
    }
}
