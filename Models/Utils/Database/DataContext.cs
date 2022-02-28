using Microsoft.EntityFrameworkCore;

namespace RoomReservations.Models.Utils.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Room?> Rooms { get; set; }
}