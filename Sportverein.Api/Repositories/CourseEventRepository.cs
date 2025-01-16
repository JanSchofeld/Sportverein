using System;
using Microsoft.EntityFrameworkCore;
using Sportverein.Api.Database;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

public class CourseEventRepository : ICourseEventRepository
{
    private readonly SportvereinDbContext dbContext;
    private readonly ICourseMembershipRepository courseMembershipRepository;

    public CourseEventRepository(SportvereinDbContext dbContext, ICourseMembershipRepository courseMembershipRepository)
    {
        this.dbContext = dbContext;
        this.courseMembershipRepository = courseMembershipRepository;
    }


    public CourseEvent Add(CourseEvent newCourseEvent)
    {
        dbContext.courseEvents.Add(newCourseEvent);
        dbContext.SaveChanges();
        return newCourseEvent;
    }

    public void Delete(int ID)
    {
        var courseEvent = dbContext.courseEvents.Find(ID);
        if (courseEvent is null){
            return;
        }

        dbContext.courseEvents.Remove(courseEvent);
        dbContext.SaveChanges();
    }

    public IEnumerable<CourseEvent> GetAll()
    {
        return dbContext.courseEvents.AsNoTracking().ToList();
    }

    public CourseEvent GetById(int ID)
    {
        return dbContext.courseEvents.AsNoTracking().Where(courseEvent => courseEvent.ID == ID).FirstOrDefault()!;
    }

    public CourseEvent Update(CourseEvent updatedCourseEvent)
    {
        dbContext.courseEvents.Update(updatedCourseEvent);
        dbContext.SaveChanges();
        return updatedCourseEvent;
    }
}
