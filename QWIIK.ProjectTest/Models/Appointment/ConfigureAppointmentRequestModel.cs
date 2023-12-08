using System.ComponentModel.DataAnnotations;

namespace QWIIK.ProjectTest.Models.Appointment
{
    public class ConfigureAppointmentRequestModel
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public bool isAvailable { get; set; }
        public string Description { get; set; } = "";
    }
}
