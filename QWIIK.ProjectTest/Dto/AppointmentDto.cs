using QWIIK.ProjectTest.Entity;
using QWIIK.ProjectTest.Models.Appointment;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWIIK.ProjectTest.Dto
{
    public class AppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public int Count { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public string Description { get; set; } = "";

        public AppointmentDto()
        {
            
        }
        public AppointmentDto(ConfigureAppointmentRequestModel model)
        {
            AppointmentDate = model.AppointmentDate;
            IsAvailable = model.isAvailable;
            Description = model.Description;
        }
    }
}
