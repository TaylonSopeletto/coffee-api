using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeApiV2.Data;
using CoffeeApiV2.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : Controller
    {
        private readonly ApiContext _context;

        public CoffeeController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("Coffee")]
        public async Task<ActionResult<List<Coffee>>> Get(int coffeeId)
        {
            var coffee = await _context.Coffees
                .Where(c => c.Id == coffeeId)
                .Include(c => c.Categories)
                .ToListAsync();

            if (coffee != null)
                return coffee;
            else return NotFound();
            

        }
        [HttpGet("Coffees")]
        public async Task<ActionResult<List<Coffee>>> Get()
        {
            var coffees = await _context.Coffees
                .Include(c => c.Categories)
                .ToListAsync();

            return coffees;


        }
    }
}

