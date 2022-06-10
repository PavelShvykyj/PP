using System.ComponentModel.DataAnnotations;

namespace PP.CoreTier.APIResourses
{
    public class CustomerResource
    {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }
            [EmailAddress]
            [Required]
            public string Email { get; set; } = null!;
    }
}
