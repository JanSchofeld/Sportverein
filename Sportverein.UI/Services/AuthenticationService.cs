using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.JsonWebTokens;
using Sportverein.Shared.Models;
using Sportverein.Shared.ViewModels;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Services;

public class AuthenticationService : Sportverein.UI.Interfaces.IAuthenticationService
{
    private readonly IAuthenticationClient authenticationClient;
    private readonly IUserClient userClient;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

    public AuthenticationService(IAuthenticationClient authenticationClient, 
                                 IUserClient userClient, 
                                 IHttpContextAccessor httpContextAccessor,
                                 JwtSecurityTokenHandler jwtSecurityTokenHandler)
    {
        this.authenticationClient = authenticationClient;
        this.userClient = userClient;
        this.httpContextAccessor = httpContextAccessor;
        this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
    }

    public async Task<User> LoginAsync(LoginViewModel loginViewModel)
    {
        var jwt = await authenticationClient.LoginAsync(loginViewModel);

        if (jwt is null){
            return null;
        }

        var context = httpContextAccessor.HttpContext;

        var token = jwtSecurityTokenHandler.ReadJwtToken(jwt);
        var claims = token.Claims.ToList();


        if(context is not null)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.Cookies.Append("Authorization", $"Bearer {jwt}");
            }

            var emailClaim = claims.Find(c => c.Type == ClaimTypes.Email);
            if(emailClaim is not null){
                var userEmail = emailClaim.Value;
                return await userClient.GetByEmailAsync(userEmail);
            }
        }

        return null!;
    }

    public async Task Logout()
    {
        var context = httpContextAccessor.HttpContext;

        if (context is not null){
            context.Response.Cookies.Delete("Authorization");
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
