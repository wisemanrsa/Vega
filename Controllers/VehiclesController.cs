using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega_be.DTOs;
using vega_be.Interfaces;
using vega_be.Models;
using vega_be.Persistance;

namespace vega_be.Controllers
{
    [Route("/api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(VegaDbContext context, IMapper mapper, IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] SaveVehicleDto vehicleDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleDto, Vehicle>(vehicleDto);
            vehicle.LastUpdate = DateTime.Now;
            context.Vehicles.Add(vehicle);
            context.SaveChanges();
            var results = mapper.Map<Vehicle, VehicleDto>(vehicle);
            return Ok(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromBody] SaveVehicleDto vehicleDto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await vehicleRepository.GetVehicle(id);
            if (vehicle == null) return NotFound();
            mapper.Map<SaveVehicleDto, Vehicle>(vehicleDto, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await context.SaveChangesAsync();
            var results = mapper.Map<Vehicle, VehicleDto>(vehicle);
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id);
            if (vehicle == null) return NotFound();
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var results = await vehicleRepository.GetVehicle(id);
            if (results == null) return NotFound();

            var vehicle = mapper.Map<Vehicle, VehicleDto>(results);

            return Ok(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await context.Vehicles.OrderBy(v => v.Id).Take(100).ToListAsync();

            var allVehicles = new List<VehicleHeader>();
            vehicles.ForEach(vehicle =>
            {
                var model = context.Models.Include(v => v.Make).SingleOrDefault(m => m.Id == vehicle.ModelId);
                allVehicles.Add(new VehicleHeader { Id = vehicle.Id, Model = model, IsRegistered = vehicle.IsRegistered });
            });
            return Ok(allVehicles);
        }
    }
}