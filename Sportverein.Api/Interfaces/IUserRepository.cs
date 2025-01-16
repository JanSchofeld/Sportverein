using System;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int ID);
    User Add(User newUser);
    User Update(User updatedUser);
    void Delete(int ID);
    User FindByEmail(string email);
    User FindByLogin(string email, string password);
}
