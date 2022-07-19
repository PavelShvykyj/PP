using System.ComponentModel.DataAnnotations;

namespace DTO.APIResourses
{
    public class SignInResource
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; } = null!;



    }
}
