using DataTier.Models;
using DTO.APIResourses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Interfaces
{
    public interface IIdentityService
    {
        SignInManager<User> SignInManager { get; }
        Task<IdentityResult> SignUpAsync(SignInResource signInData);
        Task<SignInResult> SignInAsync(LogInResource logInData);
        Task<SignInResult> SignInGoogleAsync();
        Task SendConfirmation(string callBackUrl, string email);
        Task SignOutAsync();
        Task<IdentityResult> AddToRoleAsync(SetRoleResource roleData);
        Task<IdentityResult> RemoveFromRoleAsync(SetRoleResource roleData);
        Task<IdentityResult> ChangeEmailAsync(LogInResource logInResource, ClaimsPrincipal loggedUser);
        Task<IdentityResult> SeedIdentityDataBaseAsync();
        Task<IdentityResult> FinishEmailConfirm(string userId, string code);
        Task<Dictionary<string, string>> GetConfirmUrlOptionsAsync(ClaimsPrincipal user);
    }
}
