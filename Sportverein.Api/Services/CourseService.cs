using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }

    public Course Add(Course newCourse)
    {
        return courseRepository.Add(newCourse);
    }

    public void Delete(int ID)
    {
        courseRepository.Delete(ID);
    }

    public IEnumerable<Course> GetAll()
    {
        return courseRepository.GetAll();
    }

    public Course GetById(int ID)
    {
        return courseRepository.GetById(ID);
    }

    public Course Update(Course updatedCourse)
    {
        return courseRepository.Update(updatedCourse);
    }
}
