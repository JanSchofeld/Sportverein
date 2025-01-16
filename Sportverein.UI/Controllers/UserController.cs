using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;
using Sportverein.UI.ViewModels;

namespace Sportverein.UI.Controllers;


public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }


    [HttpGet("/users")]
    [Authorize(Roles = "Admin,Trainer")]
    public async Task<IActionResult> List()
    {
        var userList = await userService.GetAllAsync();

        return View(userList);
    }

    [HttpGet("/users/edit/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int userId)
    {
        var user = await userService.GetByIdAsync(userId);
        
        return View(user);
    }

    [HttpPost("/users/update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm] User updatedUser)
    {
        var user = await userService.GetByEmailAsync(updatedUser.Email);
        updatedUser.ID = user.ID;

        await userService.UpdateAsync(updatedUser);
        return Redirect("/users");
    }

    [HttpGet("/users/new")]
    [Authorize(Roles = "Admin")]
    public IActionResult New()
    {
        return View(new User());
    }

    [HttpPost("/users/save")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save([FromForm] User newUser)
    {
        await userService.AddAsync(newUser);
        return Redirect("/users");
    }

    [HttpGet("/users/delete/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int userId)
    {
        await userService.DeleteAsync(userId);
        return Redirect("/users");
    }

    [HttpGet("/users/addtocourse/{courseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCourseMember(int courseId)
    {
        MembershipViewModel vm = new MembershipViewModel(){
            CourseId = courseId,
            Users = await userService.GetAllAsync()
        };
        return View(vm);
    }
}
