using System;
using Sportverein.Api.Interfaces;
using Sportverein.Api.Models;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;
/*
public class CourseEventMemoryRepository : ICourseEventRepository
{
    protected Dictionary<int, CourseEvent> courseEvents = new Dictionary<int, CourseEvent>();

    // Nachfragen ob Repository in ein anderes Repository injecten akzeptabel ist
    private ICourseMembershipRepository courseMembershipRepository;

    // temp hardcode
    public CourseEventMemoryRepository(ICourseMembershipRepository courseMembershipRepository)
    {
        this.courseMembershipRepository = courseMembershipRepository;

        CourseEvent ce = new CourseEvent(){CourseId = 1, Date = DateTime.Now, Description = "Norddeutsche"};
        CourseEvent ce1 = new CourseEvent(){CourseId = 2, Date = DateTime.Now, Description = "Norddeutsche"};
        CourseEvent ce2 = new CourseEvent(){CourseId = 2, Date = DateTime.Now, Description = "Regionale Meisterschaft"};

        Add(ce);
        Add(ce1);
        Add(ce2);
    }


    public CourseEvent Add(CourseEvent newCourseEvent)
    {
        newCourseEvent.ID = courseEvents.Count + 1;
        courseEvents.Add(newCourseEvent.ID, newCourseEvent);

        return newCourseEvent;
    }

    public void Delete(int ID)
    {
        courseEvents.Remove(ID);
    }

    public IEnumerable<CourseEvent> GetAll()
    {
        return courseEvents.Values;
    }

    public CourseEvent GetById(int ID)
    {
        if (courseEvents.ContainsKey(ID)){
            return courseEvents[ID];
        }

        return null!;
    }

    public CourseEvent Update(CourseEvent updatedCourseMembership)
    {
        courseEvents[updatedCourseMembership.ID] = updatedCourseMembership;

        return updatedCourseMembership;
    }

    public IEnumerable<CourseEvent> GetCourseEvents(int courseId)
    {
        var eventsOfCourse = courseEvents.Values.Where(events => events.CourseId == courseId);

        return eventsOfCourse;
    }

    public IEnumerable<CourseEvent> GetUserEvents(int userId)
    {
        /*
        var userCourseIds = courseMembershipRepository.GetUserCourses(userId);
        List<CourseEvent> userEvents = new List<CourseEvent>();

        foreach (var courseId in userCourseIds){
            var userCourses = courseEvents.Values.Where(events => events.CourseId == courseId);

            foreach (var userCourse in userCourses){
                userEvents.Add(userCourse);
            }

            //userEvents.Add((CourseEvent)courseEvents.Values.Where(events => events.CourseId == courseId));
        }

        return userEvents;
        
        throw new NotImplementedException();
    }

    
}
*/