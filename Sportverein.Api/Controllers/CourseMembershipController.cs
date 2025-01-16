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
[Route("/api/v{v:apiVersion}/coursememberships")]
public class CourseMembershipController : ControllerBase
{
    private readonly ICourseMembershipService courseMembershipService;

    public CourseMembershipController(ICourseMembershipService courseMembershipService)
    {
        this.courseMembershipService = courseMembershipService;
    }

    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseMembership))]
    public IActionResult GetAll()
    {
        return Ok(courseMembershipService.GetAll());
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(statusCode:200, type: typeof(CourseMembership))]
    [ProducesResponseType(statusCode:404, type: typeof(ProblemDetails))]
    public IActionResult GetById(int id)
    {
        var membership = courseMembershipService.GetById(id);

        if (membership is null){
            var error = new ProblemDetails() {
                Title = $"The course membership with the ID {id} was not found."
            };
            return NotFound(error);
        }

        return Ok(membership);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseMembership))]
    public IActionResult Add([FromBody] CourseMembership courseMembership)
    {
        return Ok(courseMembershipService.Add(courseMembership));
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(statusCode: 200, type: typeof(CourseMembership))]
    [ProducesResponseType(statusCode: 404, type: typeof(ProblemDetails))]
    public IActionResult Update ([FromBody] CourseMembership updatedCourseMembership)
    {
        var oldObject = courseMembershipService.GetById(updatedCourseMembership.ID);
        if (oldObject is null){
            return NotFound();
        }

        return Ok(courseMembershipService.Update(updatedCourseMembership));
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public IActionResult Delete(int id)
    {
        var obj = courseMembershipService.GetById(id);
        if (obj is null){
            return NotFound();
        }

        courseMembershipService.Delete(id);
        return Ok();
    }

    [Route("user/{userId}")]
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof (Course))]
    [ProducesResponseType(statusCode: 404, type: typeof (ProblemDetails))]
    public IActionResult GetUserCourses(int userId)
    {
        var userCourses = courseMembershipService.GetUserCourses(userId);

        if (userCourses .Count() < 1){
            var error = new ProblemDetails() {
                Title = $"The courses of the user with the ID {userId} were not found."
            };
            return NotFound(error);
        }

        return Ok(userCourses);
    }

    [Route("course/{courseId}")]
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof (User))]
    [ProducesResponseType(statusCode: 404, type: typeof (ProblemDetails))]
    public IActionResult GetCourseMembers(int courseId)
    {
        var courseMembers = courseMembershipService.GetCourseMembers(courseId);

        if (courseMembers.Count() < 1){
            var error = new ProblemDetails() {
                Title = $"The members of the course with the ID {courseId} were not found."
            };
            return NotFound(error);
        }

        return Ok(courseMembers);
    }

    [Route("course/{courseId}/trainer")]
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(User))]
    [ProducesResponseType(statusCode: 404, type: typeof (ProblemDetails))]
    public IActionResult GetCourseTrainers(int courseId)
    {
        var courseTrainers = courseMembershipService.GetCourseTrainers(courseId);

        if(courseTrainers.Count() < 1){
            var error = new ProblemDetails() {
                Title = $"The trainers of the course with the ID {courseId} could not be found."
            };
            return NotFound(error);
        }

        return Ok(courseTrainers);
    }

    [Route("{userID}/{courseID}")]
    [HttpDelete]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 404)]
    public IActionResult DeleteMembership(int userID, int courseID)
    {
        var membershipID = courseMembershipService.GetUserCourseMembershipID(userID, courseID);
        
        if (membershipID is -1){
            return NotFound();
        }

        Delete(membershipID);
        return Ok();
    }
}
