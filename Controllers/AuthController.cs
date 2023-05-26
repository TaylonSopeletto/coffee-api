using Microsoft.AspNetCore.Mvc;
using CoffeeApiV2.Models;
using CoffeeApiV2.Data;
using CoffeeApiV2.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CoffeeApiV2.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
	{
        
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(UserDTO request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User();

            user.Username = request.Username;
            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        [HttpPost("Login")]
        public ActionResult<User> Login(UserDTO request)
        {
            var user = _context.Users.FirstOrDefault(acc => acc.Username == request.Username);

            if (user != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    return BadRequest();
                }

                string token = CreateToken(user);

                return Ok(token);
            }
            else
            {
                return BadRequest("User not Found");
            }

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  user.Username)
            };

            DotNetEnv.Env.Load();
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                jwtKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
             );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }

    }
}

