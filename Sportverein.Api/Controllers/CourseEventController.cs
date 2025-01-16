using System;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Authorize]
[Route("/api/v{v:apiVersion}/events")]
public class CourseEventController : ControllerBase
{
    private readonly ICourseEventService courseEventService;

    public CourseEventController(ICourseEventService courseEventService)
    {
        this.courseEventService = courseEventService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseEvent))]
    public IActionResult GetAll()
    {
        return Ok(courseEventService.GetAll());
    }

    [Route("{id}")]
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseEvent))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult GetById(int id)
    {
        var obj = courseEventService.GetById(id);

        if (obj is null){
            var error = new ProblemDetails() {
                Title = $"The Event with the ID {id} was not found."
            };
            return NotFound(error);
        }

        return Ok(obj);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseEvent))]
    public IActionResult Add([FromBody] CourseEvent courseEvent)
    {
        var test = courseEvent;
        return Ok(courseEventService.Add(courseEvent));
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseEvent))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult Update([FromBody] CourseEvent courseEvent)
    {
        var oldObject = courseEventService.GetById(courseEvent.ID);
        if (oldObject is null){
            return NotFound();
        }

        return Ok(courseEventService.Update(courseEvent));
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public IActionResult Delete(int id)
    {
        var obj = courseEventService.GetById(id);
        if (obj is null){
            return NotFound();
        }

        courseEventService.Delete(id);
        return Ok();
    }

    [Route("courses/{courseId}")]
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseEvent))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult GetCourseEvents(int courseId)
    {
        var courseEvents = courseEventService.GetCourseEvents(courseId);
        if (courseEvents.Count() < 1){
            var error = new ProblemDetails() {
                Title = $"The events of the course with the ID {courseId} were not found."
            };
            return NotFound(error);
        }

        return Ok(courseEvents);
    }

    [Route("users/{userId}")]
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseEvent))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult GetUserEvents(int userId)
    {
        var userEvents = courseEventService.GetUserEvents(userId);

        if (userEvents is null){
            var error = new ProblemDetails() {
                Title = $"The events of the user with the ID {userId} were not found."
            };
            return NotFound(error);
        }

        return Ok(userEvents);
    }
}
