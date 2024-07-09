using BookServiceUsingRepo.Context;
using BookServiceUsingRepo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookServiceUsingRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        BookDbContext _context;
        IConfiguration _configuration;
        public LoginController(BookDbContext context,
            IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            User obj = Authenticate(user);
            if (obj != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });

            }
            return response;

        }

        User Authenticate(User user)
        {
            User obj = _context.Users.FirstOrDefault(x => x.UserName == user.UserName
          && x.Password == user.Password);
            return obj;
        }

        string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

    }

}