namespace SkillUpREST;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SkillUpREST.Repositories.Interfaces;
using SkillUpREST.Repositories.OnDrive;
using SkillUpREST.Services;
using SkillUpREST.Services.Interfaces;
using System.IO;

public static class App
{
    public static readonly string Root = "E://SkillUp.Container";
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string userRepositoryPath = Path.Combine(App.Root, "Users");
        string companyRepositoryPath = Path.Combine(App.Root, "Company");

        services.AddSingleton<IUserRepository>(new UserRepositoryOnDrive(userRepositoryPath));
        services.AddSingleton<ICompanyRepository>(new CompanyRepositoryOnDrive(companyRepositoryPath));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserBlockService, UserBlockService>();
        services.AddScoped<IUserDtoValidator, UserDtoValidator>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillUpREST", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillUpREST v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
