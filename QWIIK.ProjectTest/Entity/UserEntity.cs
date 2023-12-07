using System.ComponentModel.DataAnnotations;

namespace QWIIK.ProjectTest.Entity
{
    public class UserEntity : BaseEntity
    {
        [MaxLength(100)]
        public string UserName { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [MaxLength(100)]
        public string Password { get; set; } = "";

        public string Role { get; set; } = "";
    }
}
