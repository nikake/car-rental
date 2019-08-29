using CarRental.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task<bool> Create(Customer entity);
        Task<bool> Update(Customer entity);
        Task<bool> Delete(Customer entity);
    }
}
