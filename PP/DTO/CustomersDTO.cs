using System.ComponentModel.DataAnnotations;

namespace PP.EF.models
{
    public class CustomersDTO
    {
        [Required]
        public ushort id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [EmailAddress]
        [Required]
        public string email { get; set; } = null!;
    }
}
