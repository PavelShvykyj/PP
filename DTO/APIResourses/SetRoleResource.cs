using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.APIResourses
{
    public class SetRoleResource
    {
        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

    }
}
