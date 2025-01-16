using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.ViewModels;

public class MembershipViewModel
{
    public IEnumerable<User> Users { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
}
