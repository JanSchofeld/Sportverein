using System;
using Microsoft.AspNetCore.Mvc;
using Sportverein.Shared.Models;

namespace Sportverein.UI.Interfaces;

public interface IUserClient
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int ID);
    Task AddAsync(User newUser);
    Task UpdateAsync(User updatedUser);
    Task DeleteAsync(int ID);
    Task<User> GetByEmailAsync(string userEmail);
}
