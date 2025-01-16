using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Services;

public class CourseMembershipService : ICourseMembershipService
{
    private readonly ICourseMembershipRepository courseMembershipRepository;
    private readonly IUserRepository userRepository;
    private readonly ICourseRepository courseRepository;
    

    public CourseMembershipService(ICourseMembershipRepository courseMembershipRepository,
                                   IUserRepository userRepository,
                                   ICourseRepository courseRepository)
    {
        this.courseMembershipRepository = courseMembershipRepository;
        this.userRepository = userRepository;
        this.courseRepository = courseRepository;
    }

    public CourseMembership Add(CourseMembership newCourseMembership)
    {
        return courseMembershipRepository.Add(newCourseMembership);
    }

    public void Delete(int ID)
    {
        courseMembershipRepository.Delete(ID);
    }

    public IEnumerable<CourseMembership> GetAll()
    {
        return courseMembershipRepository.GetAll();
    }

    public CourseMembership GetById(int ID)
    {
        return courseMembershipRepository.GetById(ID);
    }

    public CourseMembership Update(CourseMembership updatedCourseMembership)
    {
        return courseMembershipRepository.Update(updatedCourseMembership);
    }

    public IEnumerable<User> GetCourseMembers(int courseId)
    {
        var courseMemberships = courseMembershipRepository.GetAll().Where(membership => membership.CourseID == courseId);
        List<User> courseMembers = new List<User>();
        foreach (var courseMember in courseMemberships){
            courseMembers.Add(userRepository.GetById(courseMember.UserID));
        }

        return courseMembers;
    }

    public IEnumerable<User> GetCourseTrainers(int courseId)
    {
        var courseMemberships = courseMembershipRepository.GetAll().Where(membership => membership.CourseID == courseId);
        var trainerIds = courseMemberships.Where(membership => membership.isTrainer == true);

        List<User> trainerList = new List<User>();

        foreach (var trainer in trainerIds){
            trainerList.Add(userRepository.GetById(trainer.UserID));
        }

        return trainerList;
    }

    public IEnumerable<Course> GetUserCourses(int userId)
    {
        var userCourseMemberships = courseMembershipRepository.GetAll().Where(membership => membership.UserID == userId);
        List<Course> userCourses = new List<Course>();

        foreach (var userCourse in userCourseMemberships){
            userCourses.Add(courseRepository.GetById(userCourse.CourseID));
        }

        return userCourses;
    }

    public int GetUserCourseMembershipID(int userId, int courseId)
    {
        var allUserMemberships = courseMembershipRepository.GetAll().Where(membership => membership.UserID == userId);
        var selectedUserMembership = allUserMemberships.Where(membership => membership.CourseID == courseId);

        var result = selectedUserMembership.FirstOrDefault();
        if (result is not null){
            return result.ID;
        }
        return -1;
    }

}
