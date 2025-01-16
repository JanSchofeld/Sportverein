using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.Interfaces;

public interface ICourseClient
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(int ID);
    Task AddAsync(Course newCourse);
    Task UpdateAsync(Course updatedCourse);
    Task DeleteAsync(int ID);
}
