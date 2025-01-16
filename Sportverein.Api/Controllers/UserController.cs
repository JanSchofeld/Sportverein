using System;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace Sportverein.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Authorize]
[Route("/api/v{v:apiVersion}/users/")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(User))]
    public IActionResult GetAll()
    {
        return Ok(userService.GetAll());
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(User))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult GetById(int id)
    {
        var userObject = userService.GetById(id);

        if (userObject is null)
        {
            var error = new ProblemDetails()
            {
                Title = $"The user with the ID {id} was not found."
            };
            return NotFound(error);
        }

        return Ok(userObject);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(User))]
    public IActionResult Add([FromBody] User newUser)
    {
        return Ok(userService.Add(newUser));
    }

    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(statusCode: 200, type: typeof(User))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult Update([FromBody] User updatedUser)
    {
        var oldObject = userService.GetById(updatedUser.ID);
        if (oldObject is null){
            return NotFound();
        }

        return Ok(userService.Update(updatedUser));
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public IActionResult Delete(int id)
    {
        var obj = userService.GetById(id);
        if (obj is null){
            return NotFound();
        }

        userService.Delete(id);
        return Ok();
    }

    [Route("email/{userEmail}")]
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: 200, type: typeof(User))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult GetByEmail(string userEmail)
    {
        var userObject = userService.FindByEmail(userEmail);

        if (userObject is null)
        {
            var error = new ProblemDetails()
            {
                Title = $"The user with the Mail {userEmail} was not found."
            };
            return NotFound(error);
        }

        return Ok(userObject);
    }
}
