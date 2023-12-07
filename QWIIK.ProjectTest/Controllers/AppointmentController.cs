using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QWIIK.ProjectTest.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public AppointmentController()
        {
            
        }

        [Authorize(Roles = "agency")]
        [HttpGet("AuthAgency")]
        public IActionResult AuthAgency()
        {
            return Ok("login");
        }

        [Authorize(Roles = "customer")]
        [HttpGet("AuthCustomer")]
        public IActionResult AuthCustomer()
        {
            return Ok("login");
        }
    }
}
