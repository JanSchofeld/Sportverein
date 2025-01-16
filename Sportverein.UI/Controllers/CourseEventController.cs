using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;
using Sportverein.UI.ViewModels;

namespace Sportverein.UI.Controllers;

public class CourseEventController : Controller
{
    private readonly ICourseEventService courseEventService;

    public CourseEventController(ICourseEventService courseEventService)
    {
        this.courseEventService = courseEventService;
    }


    [HttpGet("/events")]
    public async Task<IActionResult> List()
    {
        var courseEvents = await courseEventService.GetAllAsync();
        
        return View(courseEvents);
    }

    [HttpGet("/events/course/{courseId}")]
    public async Task<IActionResult> CourseEvents(int courseId)
    {
        var courseEvents = await courseEventService.GetByCourseIdAsync(courseId);

        return View("List", courseEvents);
    }


    [HttpGet("/events/user/{userId}")]
    [Authorize]
    public async Task<IActionResult> UserEvents(int userId)
    {
        var userEvents = await courseEventService.GetByUserIdAsync(userId);

        return View(userEvents);
    }

    [HttpGet("/events/{eventId}/edit")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int eventId)
    {
        return View(await courseEventService.GetByIdAsync(eventId));
    }

    [HttpGet("/events/{eventId}/delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int eventId)
    {
        await courseEventService.DeleteAsync(eventId);
        return Redirect("/events");
    }

    [HttpPost("/events/{eventId}/update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm] CourseEvent updatedEvent, int eventId)
    {
        var courseEvent = await courseEventService.GetByIdAsync(eventId);
        courseEvent.Description = updatedEvent.Description;
        courseEvent.Date = updatedEvent.Date;
        
        await courseEventService.UpdateAsync(courseEvent);
        return Redirect("/events");
    }

    [HttpGet("/events/new")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add()
    {
        var vm = await courseEventService.GetNewEditEventViewModelAsync();
        return View(vm);
    }

    [HttpPost("/events/save")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save([FromForm] NewEditEventViewModel newEvent)
    {
        await courseEventService.AddAsync(newEvent.CourseEvent);
        return Redirect("/events");
    }

    [HttpGet("/events/currentuser")]
    public async Task<IActionResult> CurrentUserEvents()
    {
        var currentUser = User.Claims.Where(c => c.Type == "ID").FirstOrDefault();

        if(currentUser is null){
            return Redirect("/events");
        }

        var events = await courseEventService.GetByUserIdAsync(Int32.Parse(currentUser.Value));
        return View("List", events);
    }
}
