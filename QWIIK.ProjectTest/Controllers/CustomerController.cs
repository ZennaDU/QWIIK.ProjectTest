using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QWIIK.ProjectTest.Dto;
using QWIIK.ProjectTest.EntityFramework;
using QWIIK.ProjectTest.Models.Appointment;
using QWIIK.ProjectTest.Models.User;
using QWIIK.ProjectTest.Services;
using QWIIK.ProjectTest.Utility;
using System.Security.Claims;

namespace QWIIK.ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserServices _userServices;
        private readonly AppointmentServices _appointmentServices;
        public CustomerController(ApplicationDbContext context, UserServices userServices, AppointmentServices appointmentServices)
        {
            _context = context;
            _userServices = userServices;
            _appointmentServices = appointmentServices;
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
            user.Role = AppConstant.Role.CUSTOMER_ROLE;
            user = _userServices.Register(user);


            string jwt = _userServices.CreateJwToken(user);
            var response = new
            {
                User = user,
                JWToken = jwt
            };
            return Ok(response);
        }

        [Authorize(Roles = AppConstant.Role.CUSTOMER_ROLE)]
        [HttpPost("BookAppointment")]
        public IActionResult BookAppointment([FromBody] AppointmentRequestModel appointmentRequest)
        {
            var identity = User.Identity as ClaimsIdentity;
            if(identity == null)
            {
                ModelState.AddModelError("Login", "Please Login First");
                return BadRequest(ModelState);
            }

            Dictionary<string, string> claims = new Dictionary<string, string>();
            foreach(Claim claim in identity.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            var userId = new Guid(claims["id"]);
            var user = _context.Users.FirstOrDefault(e => e.Id == userId);
            if (user == null)
            {
                ModelState.AddModelError("Login", "Please Login First");
                return BadRequest(ModelState);
            }

            //validate are this is first time appointment
            if (_context.UserAppoinments.FirstOrDefault(entity => 
            entity.AppointmentDate.Date == appointmentRequest.AppointmentDate.Date && 
            entity.UserId == userId) != null)
            {
                ModelState.AddModelError("Error", "Double Book");
                return BadRequest(ModelState);
            }

            var bookedDatetime = _appointmentServices.BookAppointment(user, appointmentRequest.AppointmentDate).ToString("yyyy-MM-dd");

            var response = new
            {
                Message = $"Booked at {bookedDatetime}"
            };
            return Ok(response);
        }
    }
}
