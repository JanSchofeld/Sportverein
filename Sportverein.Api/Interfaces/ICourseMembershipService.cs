using System;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Interfaces;

public interface ICourseMembershipService : ICourseMembershipRepository
{
    public IEnumerable<Course> GetUserCourses(int userId);
    public IEnumerable<User> GetCourseMembers(int courseId);
    public IEnumerable<User> GetCourseTrainers(int courseId);
    public int GetUserCourseMembershipID(int userId, int courseId);
}
