using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.CustomRequirement
{
    public class EmployeeRequirement : IAuthorizationRequirement
    {
        protected internal string[] _roleNames { get; set; }

        public EmployeeRequirement(string[] roleNames)
        {
            _roleNames = roleNames;
        }
    }
}
