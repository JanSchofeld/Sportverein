using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.ViewModels;

public class CourseEventViewModel
{
    public IEnumerable<CourseEvent> CourseEvents { get; set; }
    public IEnumerable<Course> Courses { get; set; }
}
