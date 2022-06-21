using CoreTier.Interfaces;
using DataTier.Models;
using DTO.APIResourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.CustomRequirement
{
    public class OwnOrdersHandler : AuthorizationHandler<EmployeeRequirement, OrderSetResource>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataService _dataService;

        public OwnOrdersHandler(UserManager<User> userManager,
                                IHttpContextAccessor contextAccessor,
                                IDataService dataService
                                )
        {
            _userManager = userManager;
            _httpContextAccessor = contextAccessor;
            _dataService = dataService;
        }
        
        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
                                                                   EmployeeRequirement requirement,
                                                                   OrderSetResource resource)
        {
            int customerId = resource.CustomerId;



            //StringValues CustomerValues;
            //if (!_httpContextAccessor
            //    .HttpContext
            //    .Request
            //    .Form
            //    .TryGetValue("CustomerId", out CustomerValues)) 
            //{
            //    return Task.CompletedTask;
            //}
            //customerId = Int32.Parse(CustomerValues[0]);
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var orderCustomer = _dataService.CustomerService.Get(customerId);


            bool isOwn = (user.Email == orderCustomer.Email);
            if (isOwn)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            foreach (var RoleName in requirement._roleNames)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, RoleName);
                if (isInRole)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}
