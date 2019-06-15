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
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork uow;

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork uow)
        {
            this.uow = uow;
            this.vehicleRepository = vehicleRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] SaveVehicleDto vehicleDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleDto, Vehicle>(vehicleDto);
            vehicle.LastUpdate = DateTime.Now;
            vehicleRepository.Add(vehicle);
            uow.Complete();
            var results = mapper.Map<Vehicle, VehicleDto>(vehicle);
            return Ok(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromBody] SaveVehicleDto vehicleDto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await vehicleRepository.Get(id);
            if (vehicle == null) return NotFound();
            mapper.Map<SaveVehicleDto, Vehicle>(vehicleDto, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await uow.Complete();
            var results = mapper.Map<Vehicle, VehicleDto>(vehicle);
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.Get(id, includeRelated: false);
            if (vehicle == null) return NotFound();
            vehicleRepository.Remove(vehicle);
            await uow.Complete();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var results = await vehicleRepository.Get(id);
            if (results == null) return NotFound();

            var vehicle = mapper.Map<Vehicle, VehicleDto>(results);

            return Ok(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await vehicleRepository.GetAll();
            return Ok(mapper.Map<List<Vehicle>, List<VehicleDto>>(vehicles));
        }
    }
}