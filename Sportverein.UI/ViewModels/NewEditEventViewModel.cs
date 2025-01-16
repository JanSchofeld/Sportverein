using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.ViewModels;

public class NewEditEventViewModel
{
    public IEnumerable<Course> Courses { get; set; }
    public CourseEvent CourseEvent { get; set; }

}
