using DTO.APIResourses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> SignUpAsync(SignInResource signInData);
        Task<IdentityResult> SeedIdentityDataBaseAsync();
        Task<SignInResult> SignInAsync(LogInResource logInData);
        Task SignOutAsync();
        Task<IdentityResult> AddToRoleAsync(SetRoleResource roleData);
        Task<IdentityResult> RemoveFromRoleAsync(SetRoleResource roleData);

    }
}
