using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Services;

public class CourseEventService : ICourseEventService
{
    private readonly ICourseEventRepository courseEventRepository;
    private readonly ICourseMembershipRepository courseMembershipRepository;

    public CourseEventService(ICourseEventRepository courseEventRepository, 
                              ICourseMembershipRepository courseMembershipRepository)
    {
        this.courseEventRepository = courseEventRepository;
        this.courseMembershipRepository = courseMembershipRepository;
    }

    public CourseEvent Add(CourseEvent newCourseEvent)
    {
        return courseEventRepository.Add(newCourseEvent);
    }

    public void Delete(int ID)
    {
        courseEventRepository.Delete(ID);
    }

    public IEnumerable<CourseEvent> GetAll()
    {
        return courseEventRepository.GetAll();
    }

    public CourseEvent GetById(int ID)
    {
        return courseEventRepository.GetById(ID);
    }

    public IEnumerable<CourseEvent> GetCourseEvents(int courseId)
    {
        // return courseEventRepository.GetCourseEvents(courseId);

        var eventList = courseEventRepository.GetAll();
        List<CourseEvent> courseEvents = new List<CourseEvent>();

        foreach (var courseEvent in eventList){
            if (courseEvent.CourseId == courseId){
                courseEvents.Add(courseEvent);
            }
        }

        return courseEvents;
    }

    public IEnumerable<CourseEvent> GetUserEvents(int userId)
    {
        List<CourseMembership> userCourses = courseMembershipRepository.GetAll().Where(membership => membership.UserID == userId).ToList();
        
        if (userCourses.Count == 0){
            return null!;
        }

        var userEvents = new List<CourseEvent>();

        foreach (var course in userCourses){
            foreach (var events in GetCourseEvents(course.CourseID)){
                userEvents.Add(events);
            }
        }
        return userEvents;
    }

    public CourseEvent Update(CourseEvent updatedCourseEvent)
    {
        return courseEventRepository.Update(updatedCourseEvent);
    }
}
