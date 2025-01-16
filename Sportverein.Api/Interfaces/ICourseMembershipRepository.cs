using System;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Interfaces;

public interface ICourseMembershipRepository
{
    public IEnumerable<CourseMembership> GetAll();
    public CourseMembership GetById(int ID);
    public CourseMembership Add(CourseMembership newCourseMembership);
    public CourseMembership Update(CourseMembership updatedCourseMembership);
    public void Delete(int ID);
}