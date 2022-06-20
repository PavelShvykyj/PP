using DataTier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.CustomRequirement
{
    internal class OwnOrdersHandler : AuthorizationHandler<EmployeeRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public OwnOrdersHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = contextAccessor;
        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeRequirement requirement)
        {

            /// request data get here
            ///_httpContextAccessor.HttpContext.Request.Body

            return Task.CompletedTask;

        }
    }
}
