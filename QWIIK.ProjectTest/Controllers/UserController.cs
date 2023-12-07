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

        [HttpPost("Register")]
        public IActionResult Register(UserModel userModel)
        {
            var userCount = _context.Users.Count(user => user.Email == userModel.Email);
            if (userCount > 0)
            {
                ModelState.AddModelError("Email", "This Email address is already used");
                return BadRequest(ModelState);
            }

            //create account
            UserDto user = new UserDto(userModel);
            user.Role = "customer";
            _userServices.Register(user);


            string jwt = _userServices.CreateJwToken(user);
            var response = new { 
                User = user,
                JWToken = jwt 
            };
            return Ok(response);
        }
    }
}
