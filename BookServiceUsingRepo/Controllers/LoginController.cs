using BookServiceUsingRepo.Context;
using BookServiceUsingRepo.Models;
using BookServiceUsingRepo.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public IActionResult Login(LoginModel user)
        {
            IActionResult response = Unauthorized();
            User obj = Authenticate(user);
            if (obj != null)
            {
                var tokenString = GenerateJSONWebToken(obj);
                response = Ok(new { token = tokenString });

            }
            return response;

        }

        User Authenticate(LoginModel user)
        {
            User obj = _context.Users.FirstOrDefault(x => x.Email == user.Email
          && x.Password == user.Password);
            return obj;
        }

        string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string RoleName = GetRoleName(user.RoleId);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, RoleName),
                new Claim(type:"DateOnly", DateTime.Now.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        string GetRoleName(int roleId)
        {
            string roleName = (from x in _context.Roles
                               where x.RoleId == roleId
                               select x.RoleName).ToString();
            return roleName;
        }

    }

}