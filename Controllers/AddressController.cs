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
                City = request.City,
                User = user
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(address);

        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Address>> Edit(AddressDTO request)
        {
            string userName = User!.Identity!.Name!;

            var user = await _context.Users
                .Where(c => c.Username == userName)
                .FirstOrDefaultAsync();

            var address = await _context.Addresses
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync();

            if(address!.User == user)
            {
                address!.PostalCode = request.PostalCode;
                address!.State = request.State;
                address!.Street = request.Street;
                address!.City = request.City;

                await _context.SaveChangesAsync();
                return Ok(address);
            }
            else
            {
                return StatusCode(401);
            }
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Address>> Delete(int id)
        {
            string userName = User!.Identity!.Name!;

            var user = await _context.Users
                .Where(c => c.Username == userName)
                .FirstOrDefaultAsync();

            var address = await _context.Addresses
                .Where(c => c.Id == id)
                .Include(c => c.User).FirstOrDefaultAsync();

            if(address!.User == user)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return StatusCode(401);
            }

        }
    }
}

