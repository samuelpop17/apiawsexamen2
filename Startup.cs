using ApiExamen2AWS.Data;
using ApiExamen2AWS.Helpers;
using ApiExamen2AWS.Models;
using ApiExamen2AWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
namespace ApiExamen2AWS;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string jsonSecrets =
            HelperSecretManager.GetSecretsAsync().GetAwaiter()
            .GetResult();
        KeysModel keysModel =
            JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);
        services.AddSingleton<KeysModel>(x => keysModel);
        string connectionString = keysModel.MySql;
        services.AddTransient<RepositoryEventos>();
        services.AddDbContext<EventosContext>
            (options => options.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)));
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Eventos AWS",
                Version = "v1"
            });
        });
        services.AddControllers();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseCors(options => options.AllowAnyOrigin());
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json",
                "ApiExamen2AWS");
            options.RoutePrefix = "";
        });
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}