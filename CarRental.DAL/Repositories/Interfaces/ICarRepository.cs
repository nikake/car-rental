using CarRental.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<Car> GetById(int id);
        Task<IEnumerable<Car>> GetAll();
        Task<IEnumerable<Car>> GetAllUnavailableCars();
        Task<bool> Create(Car entity);
        Task<bool> Update(Car entity);
        Task<bool> Delete(Car entity);
    }
}
