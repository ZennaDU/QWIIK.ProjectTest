using QWIIK.ProjectTest.Dto;

namespace QWIIK.ProjectTest.Entity
{
    public class AppointmentOptions : BaseEntity
    {
        public int MaxAppointmentPerDay { get; set; }

        public AppointmentOptions()
        {
            
        }
        public AppointmentOptions(AppointmentOptionsDto optionsDto)
        {
            MaxAppointmentPerDay = optionsDto.MaxAppointmentPerDay;
        }
    }
}
