using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _context;

        public CarRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Car entity)
        {
            _context.Cars.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Car entity)
        {
            _context.Cars.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllUnavailableCars()
        {
            return await _context.Cars.OrderBy(x => x.Id).Where(x => x.Status == Status.Unavailable).ToListAsync();
        }

        public async Task<Car> GetById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<bool> Update(Car entity)
        {
            var car = await _context.Cars.FindAsync(entity.Id);

            if(car != null) {
                car.RegistrationNumber = entity.RegistrationNumber;
                car.Status = entity.Status;
                car.CarType = entity.CarType;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
