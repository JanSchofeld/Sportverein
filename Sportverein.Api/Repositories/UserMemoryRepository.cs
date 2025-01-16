using System;
using Sportverein.Api.Interfaces;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Repositories;

/*
public class UserMemoryRepository : IUserRepository
{
    protected Dictionary<int, User> users = new Dictionary<int, User>();


    // Temporärer Konstruktor zum testen
    public UserMemoryRepository()
    {
        User u = new User(){FirstName = "Jan", LastName = "Schofeld", Email = "j.s@gmx.de"};
        User u1 = new User(){FirstName = "Max", LastName = "Mustermann", Email = "m.m@gmx.de"};
        User u2 = new User(){FirstName = "Thomas", LastName = "Schmidt", Email = "t.s@gmx.de"};
        User u3 = new User(){FirstName = "Michael", LastName = "Hartmann", Email = "m.h@gmx.de"};
        Add(u);
        Add(u1);
        Add(u2);
        Add(u3);
    }

    public User Add(User newUser)
    {
        newUser.ID = users.Count + 1;
        users.Add(newUser.ID, newUser);

        return newUser;
    }

    public void Delete(int ID)
    {
        users.Remove(ID);
    }

    public IEnumerable<User> GetAll()
    {
        return users.Values;
    }

    public User GetById(int ID)
    {
        if (users.ContainsKey(ID)){
            return users[ID];
        }

        return null!;
    }

    public User Update(User updatedUser)
    {
        users[updatedUser.ID] = updatedUser;

        return updatedUser;
    }
}
*/