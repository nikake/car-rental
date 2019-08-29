using CarRental.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration> GetById(int id);
        Task<IEnumerable<Registration>> GetAll();
        Task<bool> Create(Registration entity);
        Task<bool> Update(Registration entity);
        Task<bool> Delete(Registration entity);
    }
}
