using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sportverein.Api.Database;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

public class CourseMembershipRepository : ICourseMembershipRepository
{
    private readonly SportvereinDbContext dbContext;
    private IUserRepository userRepository;
    private ICourseRepository courseRepository;

    public CourseMembershipRepository(SportvereinDbContext dbContext, 
                                      IUserRepository userRepository,
                                      ICourseRepository courseRepository)
    {
        this.dbContext = dbContext;
        this.userRepository = userRepository;
        this.courseRepository = courseRepository;
    }

    public CourseMembership Add(CourseMembership newCourseMembership)
    {
        dbContext.CourseMemberships.Add(newCourseMembership);
        dbContext.SaveChanges();
        return newCourseMembership;
    }

    public void Delete(int ID)
    {
        var membership = dbContext.CourseMemberships.Find(ID);
        if (membership is null){
            return;
        }

        dbContext.CourseMemberships.Remove(membership);
        dbContext.SaveChanges();
    }

    public IEnumerable<CourseMembership> GetAll()
    {
        return dbContext.CourseMemberships.AsNoTracking().ToList();
    }

    public CourseMembership GetById(int ID)
    {
        return dbContext.CourseMemberships.AsNoTracking().Where(m => m.ID == ID).FirstOrDefault()!;
    }

    public CourseMembership Update(CourseMembership updatedCourseMembership)
    {
        dbContext.CourseMemberships.Update(updatedCourseMembership);
        dbContext.SaveChanges();
        return updatedCourseMembership;
    }
}
