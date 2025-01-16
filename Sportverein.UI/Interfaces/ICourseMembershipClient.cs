using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.Interfaces;

public interface ICourseMembershipClient
{
    Task<IEnumerable<CourseMembership>> GetAllAsync();
    Task<IEnumerable<User>> GetMembersByCourseIdAsync(int courseId);
    Task<IEnumerable<Course>> GetCoursesByMemberAsync(int userId);
    Task<CourseMembership> GetByIdAsync(int membershipId);
    Task AddAsync(CourseMembership newCourseMembership);
    Task UpdateAsync(CourseMembership updatedCourseMembership);
    Task DeleteAsync(int membershipId);
    Task DeleteAsync(int userId, int courseId);
}
