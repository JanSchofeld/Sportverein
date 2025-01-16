using System;
using System.ComponentModel.DataAnnotations;

namespace Sportverein.Shared.Models;

public class Course : Entity
{
    [Required]
    [StringLength(20, ErrorMessage = "Name must be between 4 and 20 characters long", MinimumLength = 4)]
    public string Name { get; set; }
}