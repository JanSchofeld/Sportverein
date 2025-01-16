using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.Interfaces;

public interface ICourseEventClient
{
    Task<List<CourseEvent>> GetAllAsync();
    Task<CourseEvent> GetByIdAsync(int eventId);
    Task<List<CourseEvent>> GetByCourseIdAsync(int courseId);
    Task<List<CourseEvent>> GetByUserIdAsync(int userId);
    Task AddAsync(CourseEvent newEvent);
    Task UpdateAsync(CourseEvent updatedEvent);
    Task DeleteAsync(int eventId);
}
