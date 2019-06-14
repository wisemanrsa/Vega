using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using vega_be.Models;

namespace vega_be.DTOs
{
    public class DetailedVehicle
    {
        public int Id { get; set; }
        public ICollection<Feature> Features { get; set; }
        public CarModel Model { get; set; }
        public ContactDto Contact { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }

        public DetailedVehicle()
        {
            Features = new Collection<Feature>();
        }
    }
}