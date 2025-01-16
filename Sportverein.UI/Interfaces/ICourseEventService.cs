using System;
using Sportverein.Shared.Models;
using Sportverein.UI.ViewModels;

namespace Sportverein.UI.Interfaces;

public interface ICourseEventService
{
    Task<CourseEventViewModel> GetAllAsync();
    Task<CourseEvent> GetByIdAsync(int eventId);
    Task<CourseEventViewModel> GetByCourseIdAsync(int courseId);
    Task<CourseEventViewModel> GetByUserIdAsync(int userId);
    Task AddAsync(CourseEvent newEvent);
    Task UpdateAsync(CourseEvent updatedEvent);
    Task DeleteAsync(int eventId);
    Task<NewEditEventViewModel> GetNewEditEventViewModelAsync();
}
