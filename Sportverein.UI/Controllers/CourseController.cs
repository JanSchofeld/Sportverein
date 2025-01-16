using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Controllers;


public class CourseController : Controller
{
    private readonly ICourseService courseService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }


    [HttpGet("/courses")]
    public async Task<IActionResult> List()
    {
        var courses = await courseService.GetAllAsync();
        return View(courses);
    }

    [HttpGet("/courses/edit/{courseID}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int courseID)
    {
        var course = await courseService.GetByIdAsync(courseID);
        return View(course);
    }

    [HttpPost("/courses/update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm] Course course)
    {
        await courseService.UpdateAsync(course);
        return Redirect("/courses");
    }

    [HttpGet("/courses/new")]
    [Authorize(Roles = "Admin")]
    public IActionResult New()
    {
        return View(new Course());
    }

    [HttpPost("/courses/save")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save([FromForm] Course course)
    {
        await courseService.AddAsync(course);
        return Redirect("/courses");
    }

    [HttpGet("/courses/{courseID}/members")]
    [Authorize(Roles = "Admin,Trainer")]
    public async Task<IActionResult> CourseMembers(int courseID)
    {
        var courseMembers = await courseService.GetCourseMembersAsync(courseID);
        
        var header = await courseService.GetByIdAsync(courseID);
        ViewBag.Header = header.Name;
        ViewBag.CourseId = courseID;
        return View(courseMembers);
    }

    [HttpGet("/courses/{courseID}/members/{userID}/delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMembership(int courseID, int userID)
    {
        await courseService.DeleteMembershipAsync(userID, courseID);
        return Redirect($"/courses/{courseID}/members");
    }

    
    [HttpGet("/courses/user/{userId}")]
    [Authorize(Roles = "Admin,Trainer")]
    public async Task<IActionResult> UserCourses(int userId)
    {
        var userCourses = await courseService.GetUserCoursesAsync(userId);
        
        return View(userCourses);
    }


    [HttpPost("/courses/{courseId}/savenewmember")]
    public async Task<IActionResult> AddCourseMember([FromForm] int userId, int courseId)
    {
        CourseMembership newMembership = new CourseMembership(){
            CourseID = courseId,
            UserID = userId
        };

        await courseService.AddCourseMemberAsync(newMembership);
        return Redirect($"/courses/{courseId}/members");
    }
}
