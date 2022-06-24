using CoreTier.Interfaces;
using CoreTier.Services;
using DTO.APIResourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PP.TierPresentation.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthenticateController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignInResource signInData)
        {


            var resoult = await _identityService.SignUpAsync(signInData);
            if (resoult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(resoult.Errors);

        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogInResource logInData)
        {
            var resoult = await _identityService.SignInAsync(logInData);
            if (resoult.Succeeded)
            {
                return Ok();
            }
            HttpContext.Response.StatusCode = 401;
            return BadRequest();
        }

        [Authorize(Policy = "OnlyAuthenticated")]
        [HttpPost]
        public async Task<IActionResult> ChangeEmailAsync(LogInResource logInData)
        {
            var resoult = await _identityService.ChangeEmailAsync(logInData, HttpContext.User);
            if (resoult.Succeeded)
            {
                return Ok();
            }
            return BadRequest(resoult);
        }

        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            await _identityService.SignOutAsync();
            return Ok();
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddToRoleAsync(SetRoleResource roleData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult res = await _identityService.AddToRoleAsync(roleData);
            if (!res.Succeeded) return BadRequest(res.Errors);
            return Ok();
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromRoleAsync(SetRoleResource roleData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult res = await _identityService.RemoveFromRoleAsync(roleData);
            if (!res.Succeeded) return BadRequest(res.Errors);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = "https://localhost:58888/API/Authenticate/GoogleResponse";
             //   Url.Action("GoogleResponse","Authenticate");
            var properties = _identityService.SignInManager
                    .ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var resoult = await _identityService.SignInGoogleAsync();
            if (resoult.Succeeded)
            {
                return Ok(HttpContext.Response.Headers.SetCookie);
            }
            return BadRequest("Access denied");
        }

        [Authorize(Policy = "OnlyAuthenticated")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail() {

            
            await _identityService.SendConfirmation(HttpContext.User);

            return Ok();
        }

        [HttpGet]
        [Route("{userId}/{code}")]
        public async Task<IActionResult> EmailConfirm(string userId, string code) {

            await _identityService.FinishEmailConfirm(userId, code);
            return Ok();
        }
    }
}
