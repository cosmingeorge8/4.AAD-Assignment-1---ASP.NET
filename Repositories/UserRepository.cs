using Castle.Core.Internal;
using RoomReservations.Interfaces;
using RoomReservations.Models;
using RoomReservations.Models.Utils;
using RoomReservations.Models.Utils.Database;

namespace RoomReservations.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        FillIfNull();
    }

    private void FillIfNull()
    {
        if (_dataContext.Users.ToList().IsNullOrEmpty())
        {
            AddHarcodedUsers();
        }
    }

    private void AddHarcodedUsers()
    {
        List<User> users = new()
    {
        new User
        {
            Username = "g.mucalau", EmailAddress = "cosminmcl@gmail.com", Password = "gica123", Name = "Cosmin Mucalau"
        },
        new User {Username = "a.sas", EmailAddress = "andreisas06@gmail.com", Password = "andrei123", Name = "Andrei Sas"}
    };
        _dataContext.Users.AddRange(users);
        _dataContext.SaveChanges();
    }



    public User GetUser(UserLogin userLogin)
    {
        var user =  _dataContext.Users.First( user=> user.Username == userLogin.Username && user.Password == userLogin.Password);
        return user;
    }

    public User GetUser(string? username)
    {
        return _dataContext.Users.First(user => user.Username == username);
    }

    public void Delete(string? username)
    {
        var user = GetUser(username);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        
        _dataContext.Users.Remove(user);
        _dataContext.SaveChanges();
    }

    public void Update(User user)
    {
        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
    }
}