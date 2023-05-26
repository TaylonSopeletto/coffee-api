using CoffeeApiV2.Data;
using CoffeeApiV2.DTOs;
using CoffeeApiV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoffeeApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeShopController : Controller
    {
        private readonly ApiContext _context;

        public CoffeeShopController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Coffee>> Add(CoffeeShop request)
        {
            var coffeeShop = new CoffeeShop
            {
                Name = request.Name,
                Rating = request.Rating,
                City = request.City
               
            };

            _context.CoffeeShops.Add(coffeeShop);
            await _context.SaveChangesAsync();

        

            await _context.SaveChangesAsync();
            return Ok(coffeeShop);

        }

        [HttpGet]
        public async Task<ActionResult<List<CoffeeShop>>> Get(int id)
        {
            var coffeeShops = await _context.CoffeeShops
                .Where(c => id == 0 || c.Id == id)
                .ToListAsync();

            if (coffeeShops != null)
                return coffeeShops;
            else return NotFound();

        }
    }
}

