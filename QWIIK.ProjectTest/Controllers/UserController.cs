using Microsoft.AspNetCore.Mvc;
using QWIIK.ProjectTest.Dto;
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

            }
            UserDto user = new UserDto() { Id = Guid.NewGuid(), Role = "customer" };
            string jwt = _userServices.CreateJwToken(user);
            var response = new { JWToken = jwt };
            return Ok(response);
        }
    }
}
