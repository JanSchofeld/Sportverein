using System;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;
using Sportverein.UI.ViewModels;

namespace Sportverein.UI.Services;

public class CourseEventService : ICourseEventService
{
    private readonly ICourseEventClient courseEventClient;
    private readonly ICourseClient courseClient;

    public CourseEventService(ICourseEventClient courseEventClient,
                              ICourseClient courseClient)
    {
        this.courseEventClient = courseEventClient;
        this.courseClient = courseClient;
    }

    public async Task AddAsync(CourseEvent newEvent)
    {
        await courseEventClient.AddAsync(newEvent);
    }

    public async Task DeleteAsync(int eventId)
    {
        await courseEventClient.DeleteAsync(eventId);
    }

    public async Task<CourseEventViewModel> GetAllAsync()
    {
        var courseEvents = await courseEventClient.GetAllAsync();
        if (courseEvents is null){
            return null!;
        }

        courseEvents.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

        var result = new CourseEventViewModel(){
            CourseEvents = courseEvents,
            Courses = await courseClient.GetAllAsync(),
        };

        return result;
    }

    public async Task<CourseEventViewModel> GetByCourseIdAsync(int courseId)
    {
        var courseEvents = await courseEventClient.GetByCourseIdAsync(courseId);
        if (courseEvents is null){
            return null!;
        }

        courseEvents.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

        var result = new CourseEventViewModel(){
            CourseEvents = courseEvents,
            Courses = await courseClient.GetAllAsync()
        };

        return result;
    }

    public async Task<CourseEvent> GetByIdAsync(int eventId)
    {
        var test = await courseEventClient.GetByIdAsync(eventId);
        return test;
    }

    public async Task<CourseEventViewModel> GetByUserIdAsync(int userId)
    {
        var userEvents = await courseEventClient.GetByUserIdAsync(userId);
        if (userEvents is null){
            return null!;
        }
        
        userEvents.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
        
        var result = new CourseEventViewModel(){
            CourseEvents = userEvents,
            Courses = await courseClient.GetAllAsync()
        };

        return result;
    }

    public async Task<NewEditEventViewModel> GetNewEditEventViewModelAsync()
    {
        var vm = new NewEditEventViewModel();
        vm.CourseEvent = new CourseEvent()
        {
            ID = 0, 
            CourseId = 0, 
            Date = DateTime.UtcNow, 
            Description = ""
        };
        vm.Courses = await courseClient.GetAllAsync();
        return vm;
    }

    public async Task UpdateAsync(CourseEvent updatedEvent)
    {
        await courseEventClient.UpdateAsync(updatedEvent);
    }
}
