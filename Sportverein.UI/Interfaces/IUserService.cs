using System;
using Sportverein.Shared.Models;

namespace Sportverein.UI.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int ID);
    Task AddAsync(User newUser);
    Task UpdateAsync(User updatedUser);
    Task DeleteAsync(int ID);
    Task<User> GetByEmailAsync(string userEmail);
}
