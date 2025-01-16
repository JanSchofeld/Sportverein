using System;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Services;

public class CourseService : ICourseService
{
    private readonly ICourseClient courseClient;
    private readonly ICourseMembershipClient courseMembershipClient;
    
    public CourseService(ICourseClient courseClient, 
                         ICourseMembershipClient courseMembershipClient)
    {
        this.courseClient = courseClient;
        this.courseMembershipClient = courseMembershipClient;
    }

    public async Task AddAsync(Course newCourse)
    {
        await courseClient.AddAsync(newCourse);
    }

    public async Task AddCourseMemberAsync(CourseMembership newMembership)
    {
        await courseMembershipClient.AddAsync(newMembership);
    }

    public async Task DeleteAsync(int ID)
    {
        await courseClient.DeleteAsync(ID);
    }

    public async Task DeleteMembershipAsync(int userId, int courseId)
    {
        await courseMembershipClient.DeleteAsync(userId, courseId);
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        var courses = await courseClient.GetAllAsync();
        if (courses.Count() == 0){
            return null!;
        }
        return courses;
    }

    public Task<Course> GetByIdAsync(int ID)
    {
        return courseClient.GetByIdAsync(ID);
    }

    public async Task<IEnumerable<User>> GetCourseMembersAsync(int courseId)
    {
        var courseMembers = await courseMembershipClient.GetMembersByCourseIdAsync(courseId);
        if (courseMembers is null){
            return null!;
        }

        return courseMembers;
    }

    public async Task<IEnumerable<Course>> GetUserCoursesAsync(int userId)
    {
        return await courseMembershipClient.GetCoursesByMemberAsync(userId);
    }

    public async Task UpdateAsync(Course updatedCourse)
    {
        await courseClient.UpdateAsync(updatedCourse);
    }
}
