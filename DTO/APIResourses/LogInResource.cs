using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.APIResourses
{
    public class LogInResource
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        public bool RememberMe { get; set; }
    }
}
