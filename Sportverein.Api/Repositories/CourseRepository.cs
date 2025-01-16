using System;
using Microsoft.EntityFrameworkCore;
using Sportverein.Api.Database;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly SportvereinDbContext dbContext;

    public CourseRepository(SportvereinDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Course Add(Course newCourse)
    {
        dbContext.Courses.Add(newCourse);
        dbContext.SaveChanges();
        return newCourse;
    }

    public void Delete(int ID)
    {
        var course = dbContext.Courses.Find(ID);
        if (course is null){
            return;
        }

        dbContext.Courses.Remove(course);
        dbContext.SaveChanges();
    }

    public IEnumerable<Course> GetAll()
    {
        return dbContext.Courses.AsNoTracking().ToList();
    }

    public Course GetById(int ID)
    {
        return dbContext.Courses.AsNoTracking().FirstOrDefault(c => c.ID == ID)!;
    }

    public Course Update(Course updatedCourse)
    {
        dbContext.Courses.Update(updatedCourse);
        dbContext.SaveChanges();
        return updatedCourse;
    }
}
