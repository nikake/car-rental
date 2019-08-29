using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly CarRentalContext _context;

        public RegistrationRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Registration entity)
        {
            _context.Registrations.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Registration entity)
        {
            _context.Registrations.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Registration>> GetAll()
        {
            return await _context.Registrations.OrderBy(x => x.Id).ToListAsync(); ;
        }

        public async Task<Registration> GetById(int id)
        {
            return await _context.Registrations.FindAsync(id);
        }

        public async Task<bool> Update(Registration entity)
        {
            var registration = await _context.Registrations.FindAsync(entity.Id);

            if(registration != null)
            {
                registration.DistanceMeter = entity.DistanceMeter;
                registration.DateTime = entity.DateTime;
                registration.RegistrationType = entity.RegistrationType;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
