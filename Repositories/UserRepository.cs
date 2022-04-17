using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using RoomReservations.Interfaces;
using RoomReservations.Models;
using RoomReservations.Models.Utils;
using RoomReservations.Models.Utils.Database;

namespace RoomReservations.Repositories;

/**
 * @author Mucalau Cosmin
 */

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        FillIfNull();
    }

    /**
     * Initialize DB with some hardcoded values
     */
    private void FillIfNull()
    {
        if (_dataContext.Users.ToList().IsNullOrEmpty())
        {
            AddHardcodedUsers();
        }
    }

    private void AddHardcodedUsers()
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



    /**
     * Get User by credentials
     */
    public Task<User> GetUser(UserLogin userLogin)
    {  
        return _dataContext.Users.FirstAsync( user=> user.Username == userLogin.Username && user.Password == userLogin.Password);
    }

    /**
     * Get user by username
     */
    public Task<User> GetUser(string? username)
    {
        return _dataContext.Users.FirstAsync(user => user.Username == username);
    }

    /**
     * delete user by username
     */
    public void Delete(string? username)
    {
        var user = GetUser(username);
        if (user.Result is null)
        {
            throw new Exception("User not found");
        }
        
        _dataContext.Users.Remove(user.Result);
        _dataContext.SaveChanges();
    }

    /**
     * Update user
     */
    public void Update(User user)
    {
        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
    }
}