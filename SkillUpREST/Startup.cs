namespace SkillUpREST;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SkillUpREST.Entity.Repository;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Services;
using SkillUpREST.Services.Interfaces;

public static class App
{
    public static readonly string Root = "E://SkillUp.Container";
    public static string UserRepository => $"{Root}/Users";
    public static string CompanyRepository => $"{Root}/Company";
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
        // I do not understand motivation here
        var userRepository = Repository.Resolve<IUserRepository>(("Location", App.UserRepository));
        var companyRepository = Repository.Resolve<ICompanyRepository>(("Location", App.CompanyRepository));
        
        services.AddSingleton<IUserRepository>(userRepository);
        services.AddSingleton<ICompanyRepository>(companyRepository);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserBlockService, UserBlockService>();
        services.AddScoped<IUserValidator, UserDtoValidator>();

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
