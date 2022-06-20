using AutoMapper;
using CoreTier.Interfaces;
using DataTier.Models;
using DTO.APIResourses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    internal class CheckedRoleData : ICheckedRoleData
    {
        public IdentityRole Role { get; set; }
        public IdentityUser User { get; set; }
        public bool IsChecked { get; set; }
    }


    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               RoleManager<IdentityRole> roleManager,
                               IDataService dataService,
                               IMapper mapper

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dataService = dataService;
            _mapper = mapper;

            
        }

        public async Task<IdentityResult> SignUpAsync(SignInResource signInData) {
            var user = await _userManager.FindByEmailAsync(signInData.Email);
            var exists = (user is not null);// || (_dataService.CustomerService.IsEmailExists(signInData.Email)); 
            if (exists)
            {
                return IdentityResult.Failed(new[] {
                    new IdentityError() {
                        Description = "Email exists already"
                        }
                });
            }

            /// mapper
            user = _mapper.Map<SignInResource, User>(signInData);
            var customer = _mapper.Map<SignInResource, Customer>(signInData);
            user.Customer = customer;
            var resoult  =  await _userManager.CreateAsync(user, signInData.Password);
            return resoult;
        }

        public async Task<SignInResult> SignInAsync(LogInResource logInData) 
        {
            var user = await _userManager.FindByEmailAsync(logInData.Email);
            var exists = (user is not null);
            if (!exists)
            {
                return SignInResult.Failed;
            }

            var resoult =  await _signInManager
                                .PasswordSignInAsync(
                                     user,
                                     logInData.Password,
                                     logInData.RememberMe,
                                     false);
            return resoult;
        }

        public async Task SignOutAsync()
        {
             await _signInManager.SignOutAsync();
        }

        private async Task<IdentityResult> CreateRoleAsync(string roleName) {
            IdentityRole role = new IdentityRole(roleName);
            var roleExists = await _roleManager.RoleExistsAsync(role.Name);

            if (!roleExists)
            {
                var res = await _roleManager.CreateAsync(role);
                return res;
            }
            return IdentityResult.Success;
        }

        public  async Task<IdentityResult> SeedIdentityDataBaseAsync() 
        {

            var adminRoleName = "Admin";
            var managerRoleName = "Manager";
            var defoultAdminName = adminRoleName;
            var defoultAdminMail = "Developers@Mail.Com";
            var defoultAdminPass = "StroNgSecu12rityPass!";

            var resoult = await CreateRoleAsync(adminRoleName);
            if (!resoult.Succeeded)
            {
                return resoult;
            }
            resoult = await CreateRoleAsync(managerRoleName);
            if (!resoult.Succeeded)
            {
                return resoult;
            }

            var adminUser = await _userManager.FindByEmailAsync(defoultAdminMail);
            if (adminUser == null) 
            {
                adminUser = new User()
                {
                    Email = defoultAdminMail,
                    UserName = defoultAdminName
                };

                resoult = await _userManager.CreateAsync(adminUser,defoultAdminPass);
                if (!resoult.Succeeded)
                {
                    return resoult;
                }
            }

            var isAdmin = await _userManager.IsInRoleAsync(adminUser, adminRoleName);
            if (!isAdmin)
            {
               resoult = await _userManager.AddToRoleAsync(adminUser, adminRoleName);
                if (!resoult.Succeeded)
                {
                    return resoult;
                }

            }
            return IdentityResult.Success;
        }

        internal async Task<ICheckedRoleData> CheckRoleData(SetRoleResource roleData) 
        {
            var resoult = new CheckedRoleData();
            resoult.User = await _userManager.FindByEmailAsync(roleData.Email);
            resoult.Role = _roleManager.Roles
                .FirstOrDefault(r => r.Name == roleData.RoleName);

            resoult.IsChecked = (resoult.User != null) & (resoult.Role != null);
            return resoult;
        }


        public async Task<IdentityResult> AddToRoleAsync(SetRoleResource roleData) 
        {
            var resoult = await CheckRoleData(roleData);
            if (!resoult.IsChecked)
            {
                return IdentityResult.Failed(new[] 
                { 
                    new IdentityError() 
                    {
                        Description = "Wrong Email or role name"
                    }
                } );
            }
            return await _userManager.AddToRoleAsync((User)resoult.User, roleData.RoleName);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(SetRoleResource roleData) 
        {
            var resoult = await CheckRoleData(roleData);
            if (!resoult.IsChecked)
            {
                return IdentityResult.Failed(new[]
                {
                    new IdentityError()
                    {
                        Description = "Wrong Email or role name"
                    }
                });
            }
            return await _userManager.RemoveFromRoleAsync((User)resoult.User, roleData.RoleName);
        }
    }

    }

