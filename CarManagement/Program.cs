using Microsoft.OpenApi.Models; 
using CarManagement.Repositories;
using CarManagement.Services;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                    services.AddControllers();

                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    services.AddScoped<ICarModelRepository>(provider => new CarModelRepository(connectionString));
                    services.AddScoped<ICarModelService, CarModelService>();
                    services.AddScoped<ISalesRepository>(provider => new SalesRepository(connectionString));
                    services.AddScoped<ISalesService, SalesService>();
                    services.AddScoped<ISalesmanInfoRepository>(provider => new SalesmanInfoRepository(connectionString));
                    services.AddScoped<ISalesmanInfoService, SalesmanInfoService>();

                    services.AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Management API", Version = "v1" });
                    });
                })
                .Configure((context, app) =>
                {
                    var env = context.HostingEnvironment;

                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();

                        app.UseSwagger();  
                        app.UseSwaggerUI(options =>
                        {
                            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Management API v1");  // Enable Swagger UI at /swagger
                        });
                    }

                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
