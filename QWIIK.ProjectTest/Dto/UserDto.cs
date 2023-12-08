using QWIIK.ProjectTest.Entity;
using QWIIK.ProjectTest.Models.User;
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
        
        public UserDto(UserModel model)
        {
            Username = model.UserName; 
            Email = model.Email;
            Password = model.Password;
        }

        public UserDto()
        {
            
        }

        public UserDto(UserEntity entity)
        {
            Id = entity.Id;
            Username = entity.UserName;
            Email = entity.Email;
            Password = entity.Password;
            Role = entity.Role;
            CreatedAt = entity.CreatedAt;
            CreatedBy = entity.CreatedBy;   
            ModifiedAt = entity.ModifiedAt;
            ModifiedBy = entity.ModifiedBy;
        }
    }
}
