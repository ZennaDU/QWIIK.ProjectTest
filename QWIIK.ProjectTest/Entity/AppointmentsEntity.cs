namespace QWIIK.ProjectTest.Entity
{
    public class AppointmentLockEntity : BaseEntity
    {
        public DateOnly AppointmentDate { get; set; }
        public int Count { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public string Description { get; set; } = "";
    }
}
