using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.CustomRequirement
{
    internal class EmployeeRequirement : IAuthorizationRequirement
    {
        protected internal IdentityRole[] _roles { get; set; }

        public EmployeeRequirement(IdentityRole[] roles)
        {
            _roles = roles;
        }
    }
}
