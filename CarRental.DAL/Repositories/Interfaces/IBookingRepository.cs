using CarRental.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetById(int id);
        Task<IEnumerable<Booking>> GetAll();
        Task<bool> Create(Booking entity);
        Task<bool> Update(Booking entity);
        Task<bool> Delete(Booking entity);
    }
}
