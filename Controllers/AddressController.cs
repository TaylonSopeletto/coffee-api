using CoffeeApiV2.Data;
using CoffeeApiV2.DTOs;
using CoffeeApiV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly ApiContext _context;

        public AddressController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<Address>> Get()
        {
            string userName = User!.Identity!.Name!;

            if(userName != null)
            {
                var addresses = await _context.Addresses
                .Where(c => c.User!.Username == userName)
                .Include(c => c.User)
                .ToArrayAsync();

                return Ok(addresses);
            }
            else
            {
                return BadRequest();
            }

            
                
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Address>> Add(AddressDTO request)
        {

            string userName = User!.Identity!.Name!;

            var user = await _context.Users
                .Where(c => c.Username == userName)
                .FirstOrDefaultAsync();

            var address = new Address
            {
                PostalCode = request.PostalCode,
                State = request.State,
                Street = request.Street,
                User = user
            };



            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(address);

        }
    }
}

