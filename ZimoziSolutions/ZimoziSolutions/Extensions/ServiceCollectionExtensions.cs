using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Core.Interfaces.Tasks;
using ZimoziSolutions.Core.Tasks;
using ZimoziSolutions.Exceptions.Api;
using ZimoziSolutions.Exceptions.Filters;
using ZimoziSolutions.Filters;
using ZimoziSolutions.Infrastructure.DbContexts;
using ZimoziSolutions.Infrastructure.Interfaces.DbContexts;
using ZimoziSolutions.Infrastructure.Interfaces.Repositories;
using ZimoziSolutions.Infrastructure.Repositories;

namespace ZimoziSolutions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureContext(this IServiceCollection services)
        {
            if (ApplicationContext.AppSetting == null)
                throw new ApiException(Constants.WrongDBConnection);

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ApplicationContext.AppSetting.SqlDbConnection, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        public static void AddPersistenceContexts(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddMemoryCache();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskCoreService, TaskCoreService>();
        }

        public static void AddMapperConfiguration(this IServiceCollection services)
        {
            var configurationMapper = new MapperConfiguration(cfg =>
            cfg.AddMaps(new[] {
                    Constants.AssemblyNameCore
                })
            );

            IMapper mapper = configurationMapper.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddFilterValidation(this IServiceCollection services)
        {
            services.AddMvc(c =>
            {
                c.Filters.Add<ExceptionControlFilterAttribute>();
                c.Filters.Add<ValidationFilter>();
                c.Filters.Add<ResultFilterAttribute>();
            });
        }
    }
}
