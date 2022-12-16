using Microsoft.EntityFrameworkCore;

namespace ReservationSystem2022.Models
{
    public class ReservationContext : DbContext
    {
        private DbSet<Reservation> reservations = null!;

        public ReservationContext (DbContextOptions<ReservationContext> options) : base (options)

        {

        }

        public DbSet<Reservation> Reservations { get => reservations; set => reservations = value; }
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;


    }

}
    





   