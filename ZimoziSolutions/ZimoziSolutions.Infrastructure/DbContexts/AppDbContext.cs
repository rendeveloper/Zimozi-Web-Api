using ZimoziSolutions.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;

namespace ZimoziSolutions.Infrastructure.DbContexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public bool HasChanges => ChangeTracker.HasChanges();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.UseSerialColumns();

            foreach (var property in builder.Model.GetEntityTypes())
            {
                property.SetTableName(property.DisplayName());
            }

            base.OnModelCreating(builder);
        }

        public DbSet<OTask> Tasks { get; set; }
    }
}
