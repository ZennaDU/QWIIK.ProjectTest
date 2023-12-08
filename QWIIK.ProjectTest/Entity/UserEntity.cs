using QWIIK.ProjectTest.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public ICollection<UserAppointments> userAppointments { get; set; } = new HashSet<UserAppointments>();

        public UserEntity()
        {
            
        }
        
        public UserEntity(UserDto userDto)
        {
            UserName = userDto.Username;
            Email = userDto.Email;
            Password = userDto.Password;
            Role = userDto.Role;
        }
    }
}
