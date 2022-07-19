using AutoMapper;
using CoreTier.Configs;
using CoreTier.Interfaces;
using DataTier.Models;
using DTO.APIResourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;

        public SignInManager<User> SignInManager { get { return _signInManager; } }

        public IdentityService(UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               RoleManager<IdentityRole> roleManager,
                               IDataService dataService,
                               IMapper mapper,
                               IHttpContextAccessor contextAccessor,
                               IEmailService emailService

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dataService = dataService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _emailService = emailService;
        }
        public async Task<IdentityResult> ChangeEmailAsync(LogInResource logInResource, ClaimsPrincipal loggedUser)
        {
            var user = await _userManager.GetUserAsync(loggedUser);
            if (user == null)
            {
                return IdentityResult.Failed(new[]
                 {
                    new IdentityError()
                    {
                        Description = "not looged in"
                    }
                });
            }
            user.Email = logInResource.Email;
            return  await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> SignUpAsync(SignInResource signInData) {
            var user = await _userManager.FindByEmailAsync(signInData.Email);
            var exists = (user is not null);
            if (exists)
            {
                return IdentityResult.Failed(new[] {
                    new IdentityError() {
                        Description = "Email exists already"
                        }
                });
            }
            user = _mapper.Map<SignInResource, User>(signInData);
            var customer = _mapper.Map<SignInResource, Customer>(signInData);
            user.Customer = customer;
            var result  =  await _userManager.CreateAsync(user, signInData.Password);
            return result;
        }
        public async Task<SignInResult> SignInAsync(LogInResource logInData) 
        {
            var user = await _userManager.FindByEmailAsync(logInData.Email);
            var exists = (user is not null);
            if (!exists)
            {
                return SignInResult.Failed;
            }

            var result =  await _signInManager
                                .PasswordSignInAsync(
                                     user,
                                     logInData.Password,
                                     logInData.RememberMe,
                                     false);
            return result;
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

            var result = await CreateRoleAsync(Constants.AdminRoleName);
            if (!result.Succeeded)
            {
                return result;
            }
            result = await CreateRoleAsync(Constants.ManagerRoleName);
            if (!result.Succeeded)
            {
                return result;
            }

            var adminUser = await _userManager.FindByEmailAsync(Constants.DefaultAdminMail);
            if (adminUser == null) 
            {
                Customer customerAdmin = new Customer()
                {
                    Email = Constants.DefaultAdminMail,
                    Name  = Constants.DefaultAdminName
                };
                
                adminUser = new User()
                {
                    Customer = customerAdmin,
                    Email = Constants.DefaultAdminMail,
                    UserName = Constants.DefaultAdminName
                };

                result = await _userManager.CreateAsync(adminUser, Constants.DefaultAdminPass);
                if (!result.Succeeded)
                {
                    return result;
                }
            }

            var isAdmin = await _userManager.IsInRoleAsync(adminUser, Constants.AdminRoleName);
            if (!isAdmin)
            {
               result = await _userManager.AddToRoleAsync(adminUser, Constants.AdminRoleName);
                if (!result.Succeeded)
                {
                    return result;
                }

            }
            return IdentityResult.Success;
        }
        internal async Task<ICheckedRoleData> CheckRoleData(SetRoleResource roleData) 
        {
            var result = new CheckedRoleData();
            result.User = await _userManager.FindByEmailAsync(roleData.Email);
            result.Role = _roleManager.Roles
                .FirstOrDefault(r => r.Name == roleData.RoleName);

            result.IsChecked = (result.User != null) & (result.Role != null);
            return result;
        }
        public async Task<IdentityResult> AddToRoleAsync(SetRoleResource roleData) 
        {
            var result = await CheckRoleData(roleData);
            if (!result.IsChecked)
            {
                return IdentityResult.Failed(new[] 
                { 
                    new IdentityError() 
                    {
                        Description = "Wrong Email or role name"
                    }
                } );
            }
            return await _userManager.AddToRoleAsync((User)result.User, roleData.RoleName);
        }
        public async Task<IdentityResult> RemoveFromRoleAsync(SetRoleResource roleData) 
        {
            var result = await CheckRoleData(roleData);
            if (!result.IsChecked)
            {
                return IdentityResult.Failed(new[]
                {
                    new IdentityError()
                    {
                        Description = "Wrong Email or role name"
                    }
                });
            }
            return await _userManager.RemoveFromRoleAsync((User)result.User, roleData.RoleName);
        }
        public async Task SendConfirmation(string callBackUrl, string email) 
        {
            string emailAddres = email;
            string emailSubject = "Confirm email in PP learn project";
            string emailBody = $"<a href='{callBackUrl}'>link</a>";
            await _emailService.SendEmailAsync(emailAddres, emailSubject, emailBody);
        }
        public async Task<SignInResult> SignInGoogleAsync()
        {
            var info =  await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) 
            {
                return SignInResult.Failed;
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
            {
                return result;
            }

            var randomPass = "GP"+Guid.NewGuid().ToString()+"!";
            var signInData = new SignInResource()
            {
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                Name = info.Principal.FindFirstValue(ClaimTypes.Email),
                Password = randomPass,
                PasswordConfirm = randomPass

            };

            var idResult = await SignUpAsync(signInData);
            if (!idResult.Succeeded) 
            {
                return SignInResult.Failed;
            }

            User user = await _userManager.FindByEmailAsync(signInData.Email);
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, false);
            return SignInResult.Success;
        }
        public async Task<IdentityResult> FinishEmailConfirm(string userId,string code) 
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }
        public async Task<Dictionary<string,string>> GetConfirmUrlOptionsAsync(ClaimsPrincipal loggedUser) 
        {
            var user = await _userManager.GetUserAsync(loggedUser);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Dictionary<string, string> opts = new Dictionary<string, string>();
            opts.Add("userId", user.Id);
            opts.Add("code", code);
            opts.Add("email", user.Email);
            return opts; 
        }
    }
}

