using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

/*
public class CourseMembershipMemoryRepository : ICourseMembershipRepository
{
    protected Dictionary<int, CourseMembership> courseMemberships = new Dictionary<int, CourseMembership>();


    // Konstruktor nur zum testen
    public CourseMembershipMemoryRepository()
    {
        CourseMembership c = new CourseMembership() {CourseID = 1, UserID = 1, isTrainer = false};
        CourseMembership c1 = new CourseMembership() {CourseID = 1, UserID = 2, isTrainer = false};
        CourseMembership c2 = new CourseMembership() {CourseID = 2, UserID = 2, isTrainer = true};
        Add(c);
        Add(c1);
        Add(c2);
    }

    public CourseMembership Add(CourseMembership newCourseMembership)
    {
        newCourseMembership.ID = courseMemberships.Count + 1;
        courseMemberships.Add(newCourseMembership.ID, newCourseMembership);

        return newCourseMembership;
    }

    public void Delete(int ID)
    {
        courseMemberships.Remove(ID);
    }

    public IEnumerable<CourseMembership> GetAll()
    {
        return courseMemberships.Values;
    }

    public CourseMembership GetById(int ID)
    {
        if (courseMemberships.ContainsKey(ID)){
            return courseMemberships[ID];
        }

        return null!;
    }

    public CourseMembership Update(CourseMembership updatedCourseMembership)
    {
        courseMemberships[updatedCourseMembership.ID] = updatedCourseMembership;

        return updatedCourseMembership;
    }

    public IEnumerable<Course> GetUserCourses(int userId)
    {
        
        var courses = courseMemberships.Values.Where(memberships => memberships.UserID == userId);
        List<int> courseIds = new List<int>();

        foreach (var courseMembership in courses){
            courseIds.Add(courseMembership.CourseID);
        }

        return courseIds;

        // return courseMemberships.Values.Where(memberships => memberships.UserID == userId);
    }

    public IEnumerable<User> GetCourseMembers(int courseId)
    {
        
        var members = courseMemberships.Values.Where(memberships => memberships.CourseID == courseId);
        List<int> userIds = new List<int>();

        foreach (var courseMembership in members){
            userIds.Add(courseMembership.UserID);
        }

        return userIds;
        
        // return courseMemberships.Values.Where(memberships => memberships.CourseID == courseId);
    }

    public IEnumerable<User> GetCourseTrainers(int courseId)
    {
        
        var course = courseMemberships.Values.Where(memberships => memberships.CourseID == courseId);

        CourseMembership? trainer = course.Where(memberships => memberships.isTrainer == true).FirstOrDefault();

        if (trainer is null){
            return -1;
        }

        return trainer.UserID;

    }
}*/