using RoomReservations.Models;

namespace RoomReservations.Repositories;

public static class UserRepository
{
    public static List<User> Users = new()
    {
        new User
        {
            Username = "g.mucalau", EmailAddress = "cosminmcl@gmail.com", Password = "gica123", Name = "Cosmin Mucalau"
        },
        new User {Username = "a.sas", EmailAddress = "andreisas06@gmail.com", Password = "andrei123", Name = "Andrei Sas"}
    };
}