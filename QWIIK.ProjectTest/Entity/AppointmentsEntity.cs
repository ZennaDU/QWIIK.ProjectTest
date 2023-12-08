using System.ComponentModel.DataAnnotations.Schema;

namespace QWIIK.ProjectTest.Entity
{
    public class AppointmentsEntity : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public int Count { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public string Description { get; set; } = "";
        [NotMapped]
        public ICollection<UserAppointments> userAppointments { get; set; } = new HashSet<UserAppointments>();
    }
}
