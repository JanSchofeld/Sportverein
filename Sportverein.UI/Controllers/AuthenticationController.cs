using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Shared.ViewModels;

namespace Sportverein.UI.Controllers;

public class AuthenticationController : Controller
{
    private readonly Interfaces.IAuthenticationService authenticationService;
    public AuthenticationController(Interfaces.IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpGet("/authentication/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/authentication/authenticate")]
    public async Task<IActionResult> Authenticate([FromForm] LoginViewModel login)
    {
        if(!ModelState.IsValid){
            return View("Login", login);
        }

        var user = await authenticationService.LoginAsync(login);
        if(user is null){
            ViewBag.Error = "Falsche Logindaten";
            return View("Login", login);
        }

        var claims = user.ToClaims();
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Redirect("/");
    }

    [HttpGet("/authentication/logout")]
    public async Task<IActionResult> Logout()
    {
        await authenticationService.Logout();
        return Redirect("/");
    }
}
