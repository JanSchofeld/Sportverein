using System;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Interfaces;

public interface ICourseRepository
{
    IEnumerable<Course> GetAll();
    Course GetById(int ID);
    Course Add(Course newCourse);
    Course Update(Course updatedCourse);
    void Delete(int ID);
}
