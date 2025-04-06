namespace ZimoziSolutions.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        public static void ConfigureOpenApi(this WebApplication app)
        {
            if(app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
        }
    }
}
