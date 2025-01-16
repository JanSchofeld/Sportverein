using System;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Interfaces;

public interface ICourseEventRepository
{
    public IEnumerable<CourseEvent> GetAll();
    public CourseEvent GetById(int ID);
    public CourseEvent Add(CourseEvent newCourseEvent);
    public CourseEvent Update(CourseEvent updatedCourseEvent);
    public void Delete(int ID);
}
