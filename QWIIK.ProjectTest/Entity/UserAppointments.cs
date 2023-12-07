namespace QWIIK.ProjectTest.Entity
{
    public class UserAppointments : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string AppointmentToken { get; set; }
    }
}
