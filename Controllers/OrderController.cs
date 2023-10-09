using CoffeeApiV2.Data;
using CoffeeApiV2.Models;
using CoffeeApiV2.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApiContext _context;

        public OrderController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Order>> Add(OrderDTO request)
        {
            string userName = User!.Identity!.Name!;

            List<int> coffeeIds = request!.Coffees!.Select(coffee => coffee.Id).ToList();

            var user = await _context.Users
                .Where(c => c.Username == userName)
                .FirstOrDefaultAsync();

            var coffees = await _context.Coffees
                .Where(c => coffeeIds.Contains(c.Id))
                .Include(c => c.Categories)
                .ToListAsync();

            var address = await _context.Addresses
                .Where(c => c.Id == request!.Address!.Id)
                .Include(c => c.User)
                .FirstOrDefaultAsync();

            int productsPrice = 0;
            double tip = productsPrice * 0.2;

            foreach (var coffee in coffees)
            {
                productsPrice += coffee.Price;
            }


            var order = new Order
            {
                PaymentMethod = request.PaymentMethod,
                Status = "PROCESSING",
                User = user,
                Coffees = coffees,
                ProductsPrice = productsPrice,
                Tip = productsPrice * 0.2,
                TotalPrice = Convert.ToInt32(productsPrice + tip),
                Address = address
                
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            return Ok(order);
        }
    }
}

