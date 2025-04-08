using ZimoziSolutions.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Domain.Models;
using ZimoziSolutions.Domain.Users;
using ZimoziSolutions.Domain.TaskHistory;
using System.Reflection;
using System.Reflection.Emit;
using ZimoziSolutions.Domain.UserTask;

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
            
            builder.Entity<UserTasks>(x => x.HasKey(p => new { p.TaskId, p.UserId }));

            builder.Entity<UserTasks>()
                .HasOne(u => u.Task)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserTasks>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OTask>()
                .HasOne<User>(s => s.AssignedUser)
                .WithMany(g => g.Tasks)
                .HasForeignKey(s => s.AssignedUserId);

            builder.Entity<OTask>()
                .HasOne<TaskComments>(s => s.TaskComments)
                .WithMany(g => g.Tasks)
                .HasForeignKey(s => s.TaskCommentsId);

            builder.Entity<OTask>()
                .HasOne<Notifications>(s => s.Notifications)
                .WithMany(g => g.Tasks)
                .HasForeignKey(s => s.NotificationsId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            List<TaskHistory> taskHistories = new List<TaskHistory>();

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                var entityName = changedEntity.Entity.GetType().Name;
                if(entityName is "OTask")
                {
                    foreach (var prop in changedEntity.Entity.GetType().GetTypeInfo().DeclaredProperties)
                    {
                        if (!prop.GetGetMethod().IsVirtual)
                        {
                            var currentValue = changedEntity.Property(prop.Name).CurrentValue;
                            switch (changedEntity.State)
                            {
                                case EntityState.Added:
                                    taskHistories.Add(new TaskHistory()
                                    {
                                        Id = 0,
                                        Timestamp = DateTime.UtcNow,
                                        FieldName = changedEntity.Property(prop.Name).Metadata.Name,
                                        OldValue = "",
                                        NewValue = currentValue.ToString(),
                                        EntityState = changedEntity.State.ToString()
                                    });

                                    break;

                                case EntityState.Modified:
                                    {
                                        var originalValue = changedEntity.GetDatabaseValues().GetValue<object>(prop.Name);
                                        if (currentValue.ToString() != originalValue.ToString())
                                        {
                                            taskHistories.Add(new TaskHistory()
                                            {
                                                Id = 0,
                                                Timestamp = DateTime.UtcNow,
                                                FieldName = changedEntity.Property(prop.Name).Metadata.Name,
                                                OldValue = originalValue.ToString(),
                                                NewValue = currentValue.ToString(),
                                                EntityState = changedEntity.State.ToString()
                                            });
                                        }
                                    }
                                    break;

                                case EntityState.Deleted:
                                    {
                                        var originalValue = changedEntity.GetDatabaseValues().GetValue<object>(prop.Name);
                                        taskHistories.Add(new TaskHistory()
                                        {
                                            Id = 0,
                                            Timestamp = DateTime.UtcNow,
                                            FieldName = changedEntity.Property(prop.Name).Metadata.Name,
                                            OldValue = originalValue.ToString(),
                                            NewValue = "",
                                            EntityState = changedEntity.State.ToString()
                                        });
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            foreach (var taskHistory in taskHistories)
            {
                base.Add(taskHistory);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<OTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskComments> TaskComments { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<TaskHistory> TaskHistory { get; set; }
    }
}
