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
[Route("/api/v{v:apiVersion}/courses/")]
public class CourseController : ControllerBase
{
    private readonly ICourseService courseService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: 200, type: typeof(Course))]
    public IActionResult GetAll()
    {
        return Ok(courseService.GetAll());
    }

    [Route("{id}")]
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: 200, type: typeof(Course))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult GetById(int id)
    {
        var obj = courseService.GetById(id);

        if(obj is null){
            var error = new ProblemDetails() {
                Title = $"The course with the ID {id} was not found."
            };
            return NotFound(error);
        }

        return Ok(obj);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(Course))]
    public IActionResult Add([FromBody]Course course)
    {
        return Ok(courseService.Add(course));
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(statusCode: 200, type: typeof(Course))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult Update([FromBody] Course updatedCourse)
    {
        var oldObject = courseService.GetById(updatedCourse.ID);
        
        if(oldObject is null){
            new ProblemDetails(){
                Title = $"The course to update with the ID {updatedCourse.ID} was not found"
            };
            return NotFound();
        }

        return Ok (courseService.Update(updatedCourse));
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public IActionResult Delete(int id)
    {
        var obj = courseService.GetById(id);
        if (obj is null){
            return NotFound();
        }

        courseService.Delete(id);
        return Ok();
    }
}
