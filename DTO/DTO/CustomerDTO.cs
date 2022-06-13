using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class CustomerDTO
    {
        [Required]
        public ushort Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
    }
}
