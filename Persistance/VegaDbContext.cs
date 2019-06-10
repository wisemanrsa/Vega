using Microsoft.EntityFrameworkCore;
using vega_be.Models;

namespace vega_be.Persistance {
    public class VegaDbContext : DbContext {
        public VegaDbContext (DbContextOptions<VegaDbContext> options) : base (options) {

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<CarModel> Models { get; set; }
    }
}