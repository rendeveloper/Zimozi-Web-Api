using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Core.Interfaces.Tasks;
using ZimoziSolutions.Core.Interfaces.Users;
using ZimoziSolutions.Core.Tasks;
using ZimoziSolutions.Core.Users;
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

        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = ApplicationContext.AppSetting.Issuer,
                    ValidateAudience = true,
                    ValidAudience = ApplicationContext.AppSetting.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(ApplicationContext.AppSetting.Token!)),
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void AddSwaggerAndSecurity(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Generated-JWT-Token Only`",
                    In = ParameterLocation.Header,
                    //Type = SecuritySchemeType.ApiKey,
                    Type = SecuritySchemeType.Http, //For prefix tag `Bearer`
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Scheme = "oauth2",
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void AddPersistenceContexts(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddMemoryCache();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskCoreService, TaskCoreService>();
            services.AddScoped<IAuthCoreService, AuthCoreService>();
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
