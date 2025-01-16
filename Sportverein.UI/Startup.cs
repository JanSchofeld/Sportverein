using System;
using Sportverein.UI.Interfaces;
using Sportverein.UI.Clients;
using Sportverein.UI.Misc;
using Sportverein.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using Sportverein.Shared.Resources;

namespace Sportverein.UI;

public class Startup
{
    private IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        this.config = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddProblemDetails();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>{
            options.LoginPath = "/authentication/login";
            options.AccessDeniedPath = "/home";
        });

        services.AddHttpContextAccessor();
        services.AddSingleton<JwtSecurityTokenHandler>();

        //services.AddScoped<HttpClient>();


        // Configure named client mit Header middleware
        services.AddTransient<AuthHeaderHandler>();
        services.AddHttpClient("Api", httpClient => {
            httpClient.BaseAddress = new Uri(ApiString.CONST_API_STRING);
        }).AddHttpMessageHandler<AuthHeaderHandler>();

        // Add Clients
        services.AddScoped<IUserClient, UserClient>();
        services.AddScoped<ICourseClient, CourseClient>();
        services.AddScoped<ICourseMembershipClient, CourseMembershipClient>();
        services.AddScoped<ICourseEventClient, CourseEventClient>();
        services.AddScoped<IAuthenticationClient, AuthenticationClient>();

        // Add Services
        services.AddScoped<ICourseEventService, CourseEventService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}