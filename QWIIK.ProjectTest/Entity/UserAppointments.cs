using System.ComponentModel.DataAnnotations.Schema;

namespace QWIIK.ProjectTest.Entity
{
    public class UserAppointments : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = new UserEntity();
        public DateTime AppointmentDate { get; set; }
        public string AppointmentToken { get; set; } = "";
    }
}
