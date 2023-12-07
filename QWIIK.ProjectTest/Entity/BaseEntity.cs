using System.ComponentModel.DataAnnotations;

namespace QWIIK.ProjectTest.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
