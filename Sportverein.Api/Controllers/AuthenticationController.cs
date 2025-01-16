using System;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Api.Interfaces;
using Sportverein.Api.Misc;
using Sportverein.Shared.ViewModels;

namespace Sportverein.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("/api/v{v:apiVersion}/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly JwtTokenHelper jwtTokenHelper;
    private IUserService userService;
    public AuthenticationController(JwtTokenHelper jwtTokenHelper, IUserService userService)
    {
        this.jwtTokenHelper = jwtTokenHelper;
        this.userService = userService;
    }

    [Route("login")]
    [HttpPost]
    public IActionResult Login([FromBody] LoginViewModel login)
    {
        if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password)){
            return BadRequest("Email or Password is empty.");
        }

        var user = userService.FindByLogin(login.Email, login.Password);
        if(user is null){
            return BadRequest("User not found.");
        }

        var token = jwtTokenHelper.CreateToken(user);
        return Ok(token);
    }

}
