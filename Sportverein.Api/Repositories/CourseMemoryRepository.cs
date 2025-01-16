using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

/*
public class CourseMemoryRepository : ICourseRepository
{
    protected Dictionary<int, Course> courses = new Dictionary<int, Course>();

    // Temporary hardcode 
    public CourseMemoryRepository()
    {
        Course c = new Course(){Name = "Basketball"};
        Course c1 = new Course(){Name = "Tennis"};
        Course c2 = new Course(){Name = "Faustball"};
        Add(c);
        Add(c1);
        Add(c2);
    }



    public Course Add(Course newCourse)
    {
        newCourse.ID = courses.Count + 1;
        courses.Add(newCourse.ID, newCourse);
        
        return newCourse;
    }

    public void Delete(int ID)
    {
        courses.Remove(ID);
    }

    public IEnumerable<Course> GetAll()
    {
        return courses.Values;
    }

    public Course GetById(int ID)
    {
        if(courses.ContainsKey(ID)){
            return courses[ID];
        }

        return null!;
    }

    public Course Update(Course updatedCourse)
    {
        courses[updatedCourse.ID] = updatedCourse;

        return updatedCourse;
    }
}
*/