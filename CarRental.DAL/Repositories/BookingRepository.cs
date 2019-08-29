using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CarRentalContext _context;

        public BookingRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Booking entity)
        {
            var car = await _context.Cars.FirstOrDefaultAsync<Car>(x => x.Id == entity.Car.Id);
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.IdentityNumber == entity.Customer.IdentityNumber);
            if (car?.Status == Status.Available)
            {
                if (customer == null)
                {
                    _context.Customers.Add(new Customer { IdentityNumber = entity.Customer.IdentityNumber });
                    await _context.SaveChangesAsync();
                    customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.IdentityNumber == entity.Customer.IdentityNumber);
                }
                entity.Customer = customer;
                _context.Bookings.Add(entity);
                car.Status = Status.Unavailable;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Booking entity)
        {
            _context.Bookings.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings
                .OrderBy(x => x.Id)
                .Where(x => x.ReturnRegistrationId == null)
                .Include(x => x.PickUpRegistration)
                .Include(x => x.ReturnRegistration)
                .Include(x => x.Car)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task<Booking> GetById(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<bool> Update(Booking entity)
        {
            try
            {
                var booking = await _context.Bookings
                .OrderBy(x => x.Id)
                .Where(x => x.ReturnRegistrationId == null)
                .Include(x => x.Car)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (booking != null)
                {
                    booking.CarId = entity.CarId;
                    booking.CustomerId = entity.CustomerId;
                    booking.PickUpRegistrationId = entity.PickUpRegistrationId;
                    booking.ReturnRegistrationId = entity.ReturnRegistrationId;
                    booking.ReturnRegistration = entity.ReturnRegistration;
                    booking.Car.Status = Status.Available;
                }

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {

            }
            return false;
        }
    }
}
