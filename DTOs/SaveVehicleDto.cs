using System.Collections.ObjectModel;
using System.Collections.Generic;
using vega_be.Models;

namespace vega_be.DTOs
{
    public class SaveVehicleDto
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactDto Contact { get; set; }
        public ICollection<int> Features { get; set; }

        public SaveVehicleDto()
        {
            Features = new Collection<int>();
        }

    }
}