using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class CustomerResource
    {
            [Required]
            [MaxLength(100)]
            public string name { get; set; }
            [EmailAddress]
            [Required]
            public string email { get; set; } = null!;
    }
}
