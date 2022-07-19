using DataTier.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Interfaces
{
    internal interface ICheckedRoleData
    {
         IdentityRole Role { get; set; }
         IdentityUser User { get; set; }

         bool IsChecked { get; set; }

    }
}
