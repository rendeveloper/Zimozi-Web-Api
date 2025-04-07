using ZimoziSolutions.Extensions;

var builder = WebApplication.CreateBuilder(args);
// add services to DI container
{
    var configuration = builder.Configuration;

    #region AppContext
    builder.Services.AddApplicationContext(configuration);
    #endregion AppContext

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddInfrastructureContext();
    builder.Services.AddJwtAuthentication();
    builder.Services.AddSwaggerAndSecurity();
    builder.Services.AddPersistenceContexts();
    builder.Services.AddRepositories();
    builder.Services.AddServices();
    builder.Services.AddMapperConfiguration();
    builder.Services.AddFilterValidation();
    builder.Services.AddOpenApi();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureOpenApi();

{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.ConfigureSwagger();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();
