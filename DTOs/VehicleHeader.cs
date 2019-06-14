using vega_be.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace vega_be.DTOs
{
    public class VehicleHeader
    {
        public int Id { get; set; }
        public CarModel Model { get; set; }
        public bool IsRegistered { get; set; }
    }
}