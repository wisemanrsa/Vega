using System.Threading.Tasks;
using vega_be.Models;

namespace vega_be.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id);
    }
}