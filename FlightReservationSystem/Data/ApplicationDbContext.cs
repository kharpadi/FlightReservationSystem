using FlightReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Route = FlightReservationSystem.Models.Route;

namespace FlightReservationSystem.Data
{
	public class ApplicationDbContext : DbContext 
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {
            
        }

        public DbSet<User> users { get; set; }
        public DbSet<Plane> planes { get; set; }
        public DbSet<Reservation>reservations { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        public DbSet<Route> routes { get; set; }
    }
}
