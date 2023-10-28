using Microsoft.AspNetCore.Mvc;
using CoffeeApiV2.Data;
using CoffeeApiV2.Models;
using Microsoft.EntityFrameworkCore;
using CoffeeApiV2.DTOs;

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
            try
            {               
                List<int> categoryIds = request!.Categories!.Select(category => category.Id).ToList();

                var categories = await _context.Categories
                    .Where(c => categoryIds != null && categoryIds.Contains(c.Id))
                    .Include(c => c.Coffees)
                    .ToListAsync();

                var coffee = new Coffee
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Categories = categories
                };

                _context.Coffees.Add(coffee);
                await _context.SaveChangesAsync();

                return Ok(coffee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpGet]
        public async Task<ActionResult<List<Coffee>>> Get(int id, string? name, string? category)
        {
            try
            {
                var query = _context.Coffees
                    .Where(c => id == 0 || c.Id == id)
                    .Where(c => name == null || c.Name == name);

                if (!string.IsNullOrEmpty(category))
                {
                    query = query
                        .Where(c => c.Categories!.Any(cat => cat.Name == category));
                }

                var coffees = await query.Include(c => c.Categories).ToListAsync();
                return Ok(coffees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpPut]
        public async Task<ActionResult<Coffee>> Edit(EditCoffeeDTO request)
        {
            try
            {
                List<int> categoryIds = request!.Categories!.Select(category => category.Id).ToList();

                var categories = await _context.Categories
                    .Where(c => categoryIds != null && categoryIds.Contains(c.Id))
                    .Include(c => c.Coffees)
                    .ToListAsync();

                var coffee = await _context.Coffees
                    .Where(c => c.Id == request.Id)
                    .Include(c => c.Categories)
                    .FirstOrDefaultAsync();

                if (coffee == null)
                    return NotFound();

                coffee.Name = request.Name;
                coffee.Price = request.Price;
                coffee.Description = request.Description;
                coffee.Categories = categories;

                await _context.SaveChangesAsync();
                return Ok(coffee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Coffee>> Delete(int id)
        {
            try
            {
                var coffee = await _context.Coffees
                .Where(c => c.Id == id)
                .Include(c => c.Categories).FirstOrDefaultAsync();

                if (coffee == null)
                    return NotFound();

                if (coffee != null)
                {
                    _context.Coffees.Remove(coffee);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

