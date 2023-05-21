using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var coffee = new Coffee
            {
                Name = request.Name,
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
                    var currentCategory = await _context.Categories.Where(c => c.Id == category.Id).ToListAsync();
                    if (newCoffee.Categories != null && currentCategory != null)
                        newCoffee.Categories.Add(currentCategory[0]);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
            return coffee;

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
        [HttpGet]
        public async Task<ActionResult<List<Coffee>>> Get()
        {
            var coffees = await _context.Coffees
                .Include(c => c.Categories)
                .ToListAsync();

            return coffees;
        }

        [HttpPut]
        public async Task<ActionResult<Coffee>> Edit(EditCoffeeDTO request)
        {
            var coffee = await _context.Coffees
                .Where(c => c.Id == request.Id)
                .Include(c => c.Categories).FirstOrDefaultAsync();
            if (coffee == null)
                return NotFound();

            coffee.Name = request.Name;
            coffee.Price = request.Price;
            if (coffee.Categories != null)
             coffee.Categories.RemoveAll(c => 1 == 1);

           
            if(request.Categories != null)
            {
                foreach(CoffeeCategoryDTO category in request.Categories)
                {
                    var currentCategory = await _context.Categories.Where(c => c.Id == category.Id).ToListAsync();
                        if(coffee.Categories != null && currentCategory != null)
                        coffee.Categories.Add(currentCategory[0]);
                        await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
            return coffee;
        }

        [HttpDelete]
        public async Task<ActionResult<Coffee>> Delete(int coffeeId)
        {
            var coffee = await _context.Coffees
                .Where(c => c.Id == coffeeId)
                .Include(c => c.Categories).FirstOrDefaultAsync();

            if (coffee == null)
                NotFound();

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

