using System;
using System.ComponentModel.DataAnnotations;

namespace Sportverein.Shared.Models;

public class CourseEvent : Entity
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Description must be between 5 and 100 Characters", MinimumLength = 5)]
    public string Description { get; set; }
}
