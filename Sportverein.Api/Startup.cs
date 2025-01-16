using System;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.OpenApi.Models;
using Sportverein.Api.Models;
using Sportverein.Api.Interfaces;
using Sportverein.Api.Database;
using Sportverein.Api.Repositories;
using Sportverein.Api.Services;
using Sportverein.Api.Misc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;

namespace Sportverein.Api;

public class Startup
{
    private IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        config = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddProblemDetails();

        services.Configure<DatabaseSettings>(options => config.GetSection("DatabaseSettings").Bind(options));
        services.Configure<JwtTokenSettings>(options => config.GetSection("JwtTokenSettings").Bind(options));

        services.AddSingleton<JwtTokenHelper>();

        services.AddSwaggerGen(c=> {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SportvereinApi v1", Version = "v1"});
        });

        services.AddApiVersioning(options => {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options => {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        // Memory Repositories
        //services.AddSingleton<ICourseRepository, CourseMemoryRepository>();
        //services.AddSingleton<IUserRepository, UserMemoryRepository>();
        //services.AddSingleton<ICourseMembershipRepository, CourseMembershipMemoryRepository>();
        //services.AddSingleton<ICourseEventRepository, CourseEventMemoryRepository>();

        services.AddSingleton<DbConnectionFactory>();

        // Add EF Core Repositories
        services.AddScoped<SportvereinDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseMembershipRepository, CourseMembershipRepository>();
        services.AddScoped<ICourseEventRepository, CourseEventRepository>();

        // Add Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseEventService, CourseEventService>();
        services.AddScoped<ICourseMembershipService, CourseMembershipService>();

        var settings = config.GetSection("JwtTokenSettings").Get<JwtTokenSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "Sportverein.Api",
                ValidAudience = "Sportverein.Api",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.EncryptionKey))
            };
        });
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        if(env.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","SportvereinApi v1");
            });
        }
    }
}
