using CoreTier.Interfaces;
using CoreTier.Services;
using DTO.APIResourses;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> SignUpAsync(SignInResource signInData) {


            var resoult = await _identityService.SignUpAsync(signInData);
            if (resoult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(resoult.Errors);
        
        }

    
    }
}
