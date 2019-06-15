using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega_be.Interfaces;
using vega_be.Models;

namespace vega_be.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> Get(int id, bool includeRelated = true)
        {
            if (!includeRelated) return await context.Vehicles.FindAsync(id);
            return await context.Vehicles
                .Include(v => v.Features).ThenInclude(f => f.Feature)
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}