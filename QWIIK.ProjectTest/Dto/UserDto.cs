using System.ComponentModel.DataAnnotations;

namespace QWIIK.ProjectTest.Dto
{

    public class UserDto : BaseEntityDto
    {
        [MaxLength(100)]
        public string Username { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [MaxLength(100)]
        public string Password { get; set; } = "";

        public string Role { get; set; } = "";
    }
}
