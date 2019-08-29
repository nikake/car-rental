using CarRental.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Car

            modelBuilder.Entity<Car>()
                .ToTable("Cars");
            modelBuilder.Entity<Car>()
                .Property(car => car.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Car>()
                .Property(car => car.CarType)
                .HasConversion<int>();
            modelBuilder.Entity<Car>()
                .HasAlternateKey(car => car.RegistrationNumber);

            #endregion

            #region Customer

            modelBuilder.Entity<Customer>()
                .ToTable("Customers");
            modelBuilder.Entity<Customer>()
                .Property(customer =>  customer.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>()
                .HasAlternateKey(customer => customer.IdentityNumber);

            #endregion

            #region Registration

            modelBuilder.Entity<Registration>()
                .ToTable("Registrations");
            modelBuilder.Entity<Registration>()
                .Property(registration => registration.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Registration>()
                .Property(registration => registration.RegistrationType)
                .HasConversion<int>();

            #endregion

            #region Booking

            modelBuilder.Entity<Booking>()
                .ToTable("Bookings");
            modelBuilder.Entity<Booking>()
                .Property(booking => booking.Id)
                .ValueGeneratedOnAdd();

            #endregion
        }
    }
}
