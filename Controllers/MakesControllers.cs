using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega_be.DTOs;
using vega_be.Models;
using vega_be.Persistance;

namespace vega_be.Controllers
{
    [Route("/api/[controller]")]
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairDto>> GetMakes()
        {
            var makes = await this.context.Makes.ToListAsync();
            return mapper.Map<List<Make>, List<KeyValuePairDto>>(makes);
        }

        [HttpGet("{makeId}/models")]
        public async Task<IEnumerable<CarModel>> GetModels(int makeId)
        {
            return await context.Models.Where(model => model.MakeId.Equals(makeId)).ToListAsync();
        }
    }
}