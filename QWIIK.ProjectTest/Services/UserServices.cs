using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QWIIK.ProjectTest.Dto;
using QWIIK.ProjectTest.Entity;
using QWIIK.ProjectTest.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QWIIK.ProjectTest.Services
{
    public class UserServices
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public UserServices(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string CreateJwToken(UserDto userDto)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", "" + userDto.Id),
                new Claim("role", "" + userDto.Role)
            };

            string strKey = _configuration["JwtSettings:Key"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audiance"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public UserDto Register(UserDto userDto)
        {
            //create account
            UserEntity user = new UserEntity(userDto);

            //encrypted password
            var passwordHasher = new PasswordHasher<UserEntity>();
            var encrypptedPassword = passwordHasher.HashPassword(user, userDto.Password);
            user.Password = encrypptedPassword;

            _context.Users.Add(user);
            _context.SaveChanges();

            userDto = new UserDto(user);
            return userDto;
        }
    }
}
