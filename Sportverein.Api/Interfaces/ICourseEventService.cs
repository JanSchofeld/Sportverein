using System;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Interfaces;

public interface ICourseEventService : ICourseEventRepository
{
    public IEnumerable<CourseEvent> GetUserEvents(int userId);
    public IEnumerable<CourseEvent> GetCourseEvents(int courseId);
}
