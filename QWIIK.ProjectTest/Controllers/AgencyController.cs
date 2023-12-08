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
    public class AgencyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserServices _userServices;
        private readonly AppointmentServices _appointmentServices;
        public AgencyController(ApplicationDbContext context, UserServices userServices, AppointmentServices appointmentServices)
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
            user.Role = AppConstant.Role.AGENCY_ROLE;
            user = _userServices.Register(user);


            string jwt = _userServices.CreateJwToken(user);
            var response = new
            {
                User = user,
                JWToken = jwt
            };
            return Ok(response);
        }

        [Authorize(Roles = AppConstant.Role.AGENCY_ROLE)]
        [HttpGet("GetAppointmentOptions")]
        public IActionResult GetAppointmentOptions()
        {
            var response = new
            {
                MaxAppointmentPerDay = _appointmentServices.GetAppointmentMaxDay()
            };
            return Ok(response);
        }

        [Authorize(Roles = AppConstant.Role.AGENCY_ROLE)]
        [HttpPost("UpdateAppointmentOptions")]
        public IActionResult UpdateAppointmentOptions([FromBody] AppointmentOptionsModel optionsModel) 
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                ModelState.AddModelError("Login", "Please Login First");
                return BadRequest(ModelState);
            }

            Dictionary<string, string> claims = new Dictionary<string, string>();
            foreach (Claim claim in identity.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            var userId = claims["id"];

            AppointmentOptionsDto appointmentOptionsDto = new AppointmentOptionsDto(optionsModel);
            _appointmentServices.UpdateAppointmentMaxDay(appointmentOptionsDto, userId);

            var response = new
            {
                AppointmentOptionsDto = optionsModel
            };
            return Ok(response);
        }

        [Authorize(Roles = AppConstant.Role.AGENCY_ROLE)]
        [HttpPost("GetUserAppointments")]
        public IActionResult GetUserAppointments([FromBody] AppointmentRequestModel appointmentRequest)
        {
            var users = _appointmentServices.GetUserAppointments(appointmentRequest.AppointmentDate);
            var response = new
            {
                Customers = users
            };
            return Ok(response);
        }

        [Authorize(Roles = AppConstant.Role.AGENCY_ROLE)]
        [HttpPost("ConfigureAppointments")]
        public IActionResult ConfigureAppointments([FromBody] ConfigureAppointmentRequestModel configureAppointmentRequest)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                ModelState.AddModelError("Login", "Please Login First");
                return BadRequest(ModelState);
            }

            Dictionary<string, string> claims = new Dictionary<string, string>();
            foreach (Claim claim in identity.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            var userId = new Guid(claims["id"]);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                ModelState.AddModelError("Login", "Please Login First");
                return BadRequest(ModelState);
            }

            _appointmentServices.ConfigureAppointments(new UserDto(user), new AppointmentDto(configureAppointmentRequest));
            var response = new
            {
                Customers = configureAppointmentRequest
            };
            return Ok(response);
        }
    }
}
