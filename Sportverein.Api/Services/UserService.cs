using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public User Add(User newUser)
    {
        return userRepository.Add(newUser);
    }

    public void Delete(int ID)
    {
        userRepository.Delete(ID);
    }

    public User FindByEmail(string email)
    {
        return userRepository.FindByEmail(email);
    }

    public User FindByLogin(string email, string password)
    {
        return userRepository.FindByLogin(email, password);
    }

    public IEnumerable<User> GetAll()
    {
        return userRepository.GetAll();
    }

    public User GetById(int ID)
    {
        return userRepository.GetById(ID);
    }

    public User Update(User updatedUser)
    {
        return userRepository.Update(updatedUser);
    }
}
