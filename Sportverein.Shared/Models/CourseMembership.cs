using System;
using System.ComponentModel.DataAnnotations;

namespace Sportverein.Shared.Models;

public class CourseMembership : Entity
{
    [Required]
    public int UserID { get; set; }
    [Required]
    public int CourseID { get; set; }
    public bool isTrainer { get; set; } = false;
}
