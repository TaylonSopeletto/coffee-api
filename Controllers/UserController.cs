using CoffeeApiV2.Data;
using CoffeeApiV2.DTOs;
using CoffeeApiV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeApiV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost, Authorize(Roles = "admin")]
        public ActionResult<User> Create(UserDTO request)
        {
            try
            {
                string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new User
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    Role = request.Role
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut, Authorize(Roles = "admin")]
        public ActionResult<User> Update(UserDTO request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(acc => acc.Id == request.Id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    user.Username = request.Username;
                    user.Role = request.Role;

                    _context.SaveChanges();

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }
    }
}
