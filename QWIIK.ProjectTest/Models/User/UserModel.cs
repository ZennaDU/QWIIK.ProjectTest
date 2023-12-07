using System.ComponentModel.DataAnnotations;

namespace QWIIK.ProjectTest.Models.User
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please provide your Username")]
        public string UserName { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(5, ErrorMessage = "Must be minimum 5 character")]
        [MaxLength(12, ErrorMessage = "Must be maximum 1000 character")]
        public string Password { get; set; } = "";
        [Required]
        [MinLength(5, ErrorMessage = "Must be minimum 5 character")]
        [MaxLength(1000, ErrorMessage = "Must be maximum 1000 character")]
    }
}
