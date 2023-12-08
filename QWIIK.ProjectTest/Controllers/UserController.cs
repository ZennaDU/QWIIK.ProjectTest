using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QWIIK.ProjectTest.Dto;
using QWIIK.ProjectTest.Entity;
using QWIIK.ProjectTest.EntityFramework;
using QWIIK.ProjectTest.Models.User;
using QWIIK.ProjectTest.Services;

namespace QWIIK.ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserServices _userServices;
        public UsersController(ApplicationDbContext context, UserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserName == username);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Username are not exist");
                return BadRequest(ModelState);
            }

            //verify password
            var passwordHasher = new PasswordHasher<UserEntity>();
            var result = passwordHasher.VerifyHashedPassword(new UserEntity(), user.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Wrong Password");
                return BadRequest(ModelState);
            }

            string jwt = _userServices.CreateJwToken(new UserDto(user));
            var response = new
            {
                User = user,
                JWToken = jwt
            };
            return Ok(response);
        }
    }
}
