using CoffeeApiV2.Data;
using CoffeeApiV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
            try
            {
                var coffeeShop = new CoffeeShop
                {
                    Name = request.Name,
                    Rating = request.Rating,
                    City = request.City
                };

                _context.CoffeeShops.Add(coffeeShop);
                await _context.SaveChangesAsync();

                return Ok(coffeeShop);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<List<CoffeeShop>>> Get(int id)
        {
            try
            {
                var coffeeShops = await _context.CoffeeShops
                    .Where(c => id == 0 || c.Id == id)
                    .Include(c => c.Ratings!)
                    .ThenInclude(r => r.User)
                    .ToListAsync();

                if (coffeeShops != null)
                    return coffeeShops;
                else return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<CoffeeShop>> Edit(CoffeeShop request)
        {
            try
            {
                var coffeeShop = await _context.CoffeeShops
                   .Where(c => c.Id == request.Id)
                   .FirstOrDefaultAsync();

                if (coffeeShop == null)
                    return NotFound();

                coffeeShop.Name = request.Name;
                coffeeShop.Rating = request.Rating;
                coffeeShop.City = request.City;

                await _context.SaveChangesAsync();
                return Ok(coffeeShop);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpDelete]
        public async Task<ActionResult<CoffeeShop>> Delete(int id)
        {
            try
            {
                var coffeeShop = await _context.CoffeeShops
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();


                if (coffeeShop == null)
                    return NotFound();

                if (coffeeShop != null)
                {
                    _context.CoffeeShops.Remove(coffeeShop);
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

        [HttpPost("Rate"), Authorize]
        public async Task<ActionResult<CoffeeShop>> Add(int star, string comment, int shopId )
        {
            try
            {
                string userName = User!.Identity!.Name!;

                var user = await _context.Users
                    .Where(c => c.Username == userName)
                    .FirstOrDefaultAsync();

                var shop = await _context.CoffeeShops
                    .Where(c => c.Id == shopId)
                    .Include(c => c.Ratings)
                    .FirstOrDefaultAsync();

                var rating = new Rating
                {
                    Comment = comment,
                    Star = star,
                    User = user
                };

                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();

                shop!.Ratings!.Add(rating);

                await _context.SaveChangesAsync();

                return Ok(shop);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        
    }
}

