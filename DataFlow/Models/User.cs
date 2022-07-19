using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Models
{
    public class User : IdentityUser
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
