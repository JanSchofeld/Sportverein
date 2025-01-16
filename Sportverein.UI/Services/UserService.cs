using System;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Services;

public class UserService : IUserService
{
    private readonly IUserClient userClient;

    public UserService(IUserClient userClient)
    {
        this.userClient = userClient;
    }


    public async Task AddAsync(User newUser)
    {
        await userClient.AddAsync(newUser);
    }

    public async Task DeleteAsync(int ID)
    {
        await userClient.DeleteAsync(ID);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var userList = await userClient.GetAllAsync();
        return userList;
    }

    public async Task<User> GetByEmailAsync(string userEmail)
    {
        return await userClient.GetByEmailAsync(userEmail);
    }

    public Task<User> GetByIdAsync(int ID)
    {
        return userClient.GetByIdAsync(ID);
    }

    public async Task UpdateAsync(User updatedUser)
    {
        var oldUser = await userClient.GetByEmailAsync(updatedUser.Email);
        updatedUser.PasswordHash = oldUser.PasswordHash;
        
        await userClient.UpdateAsync(updatedUser);
    }
}
