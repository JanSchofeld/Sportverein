using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(int ID);
    Task AddAsync(Course newCourse);
    Task UpdateAsync(Course updatedCourse);
    Task DeleteAsync(int ID);
    Task<IEnumerable<User>> GetCourseMembersAsync(int courseId);
    Task DeleteMembershipAsync(int userId, int courseId);
    Task<IEnumerable<Course>> GetUserCoursesAsync(int userId);
    Task AddCourseMemberAsync(CourseMembership newMembership);
}
