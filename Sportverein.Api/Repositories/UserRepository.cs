using System;
using Microsoft.EntityFrameworkCore;
using Sportverein.Api.Database;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SportvereinDbContext dbContext;

    public UserRepository(SportvereinDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public User Add(User newUser)
    {
        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();
        return newUser;
    }

    public void Delete(int ID)
    {
        var user = dbContext.Users.Find(ID);
        if (user is null){
            return;
        }

        dbContext.Users.Remove(user);
        dbContext.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return dbContext.Users.AsNoTracking().ToList();
    }

    public User Update(User updatedUser)
    {
        dbContext.Users.Update(updatedUser);
        dbContext.SaveChanges();
        return updatedUser;
    }

    public User GetById(int ID)
    {
        return dbContext.Users.AsNoTracking().FirstOrDefault(u => u.ID == ID)!;
    }

    public User FindByEmail(string email)
    {
        return dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Email == email)!;
    }

    public User FindByLogin(string email, string password)
    {
        return dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Email == email && u.PasswordHash == password)!;
    }
}
