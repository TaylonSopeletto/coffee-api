using Microsoft.AspNetCore.Mvc;
using CoffeeApiV2.Data;
using CoffeeApiV2.Models;
using Microsoft.EntityFrameworkCore;
using CoffeeApiV2.DTOs;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost]
        public async Task<ActionResult<Coffee>> Add(EditCoffeeDTO request)
        {
            var coffee = new Coffee
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _context.Coffees.Add(coffee);
            await _context.SaveChangesAsync();

            var newCoffee = await _context.Coffees
                .Where(c => c.Id == coffee.Id)
                .Include(c => c.Categories).FirstOrDefaultAsync();

            if (request.Categories != null && newCoffee != null)
            {
                foreach (CoffeeCategoryDTO category in request.Categories)
                {
                    var currentCategory = await _context.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();
                    if (newCoffee.Categories != null && currentCategory != null)
                        newCoffee.Categories.Add(currentCategory);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
            return coffee;

        }

        [HttpGet("sumProducts")]
        public async Task<ActionResult<List<CartDTO>>> Get([FromQuery] List<int> ids)
        {
            var coffees = await _context.Coffees
                .Where(c => ids.Contains(c.Id))
                .Include(c => c.Categories)
                .ToListAsync();

            int productsPrice = 0;

            foreach (var coffee in coffees)
            {
                productsPrice += coffee.Price;
            }

            double tip = productsPrice * 0.2;


            var cart = new CartDTO {
                Coffees = coffees,
                ProductsPrice = productsPrice,
                Tip = productsPrice * 0.2,
                TotalPrice = Convert.ToInt32(productsPrice + tip)
            };

            return Ok(cart);
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<Coffee>>> Get(int id, string? name, string? category)
        {
            var query = _context.Coffees
            .Where(c => id == 0 || c.Id == id)
            .Where(c => name == null || c.Name == name);

            if (!string.IsNullOrEmpty(category))
            {
                query = query
                    .Where(predicate: c => c.Categories.Any(cat => cat.Name == category));
            }

            var coffees = await query.Include(c => c.Categories).ToListAsync();
            return Ok(coffees);
            
        }

        [HttpPut]
        public async Task<ActionResult<Coffee>> Edit(EditCoffeeDTO request)
        {
            var coffee = await _context.Coffees
                .Where(c => c.Id == request.Id)
                .Include(c => c.Categories)
                .FirstOrDefaultAsync();

            if (coffee == null)
                return NotFound();

            coffee.Name = request.Name;
            coffee.Price = request.Price;
            coffee.Description = request.Description;
            if (coffee.Categories != null)
                coffee.Categories.RemoveAll(c => 1 == 1);

           
            if(request.Categories != null)
            {
                foreach(CoffeeCategoryDTO category in request.Categories)
                {
                    var currentCategory = await _context.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();
                        if(coffee.Categories != null && currentCategory != null)
                        coffee.Categories.Add(currentCategory);
                        await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
            return coffee;
        }

        [HttpDelete]
        public async Task<ActionResult<Coffee>> Delete(int id)
        {
            var coffee = await _context.Coffees
                .Where(c => c.Id == id)
                .Include(c => c.Categories).FirstOrDefaultAsync();

            if (coffee == null)
                return NotFound();

            if(coffee != null)
            {
                _context.Coffees.Remove(coffee);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return NoContent();
         
        }

        
    }
}

