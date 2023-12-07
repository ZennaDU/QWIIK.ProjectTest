namespace QWIIK.ProjectTest.Entity
{
    public class UserAppointments : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentToken { get; set; } = "";
    }
}
