using System.Threading.Tasks;
using vega_be.Models;

namespace vega_be.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> Get(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}