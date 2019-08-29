using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalContext _context;

        public CustomerRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Customer entity)
        {
            _context.Customers.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Customer entity)
        {
            _context.Customers.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> Update(Customer entity)
        {
            var customer = await  _context.Customers.FindAsync(entity.Id);

            if(customer != null)
            {
                customer.IdentityNumber = entity.IdentityNumber;
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
