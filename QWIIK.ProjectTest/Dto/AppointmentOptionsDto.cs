using QWIIK.ProjectTest.Models.Appointment;

namespace QWIIK.ProjectTest.Dto
{
    public class AppointmentOptionsDto
    {
        public int MaxAppointmentPerDay { get; set; }

        public AppointmentOptionsDto()
        {
            
        }
        public AppointmentOptionsDto(AppointmentOptionsModel model)
        {
            MaxAppointmentPerDay = model.MaxAppointmentPerDay;
        }
    }
}
