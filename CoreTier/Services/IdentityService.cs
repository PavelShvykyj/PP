using AutoMapper;
using CoreTier.Interfaces;
using DataTier.Models;
using DTO.APIResourses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly DataService _dataService;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               //DataService dataService,
                               IMapper mapper

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_dataService = dataService;
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
    }
}
