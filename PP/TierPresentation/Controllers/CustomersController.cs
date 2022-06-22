using Microsoft.AspNetCore.Mvc;
using DTO.APIResourses;
using DTO.DTO;
using CoreTier.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _dataService;

        public CustomersController(IDataService dataService)
        {
            _dataService = dataService.CustomerService;
        }

        [Authorize(Policy = "OnlyEmployee")]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerResource CustomerResource)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }
            CustomerDTO customerDTO = await _dataService.CreateAsync(CustomerResource);
            return Ok(customerDTO);
        }

        [Authorize(Policy = "OnlyEmployee")]
        [HttpPost]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update(int id, CustomerResource CustomerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CustomerDTO customerDTO = await _dataService.UpdateAsync(id, CustomerResource);
            return Ok(customerDTO);
        }

        [Authorize(Policy = "Onlyauthenticated")]
        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            CustomerDTO[] customersDTOs = (CustomerDTO[])_dataService.GetList(skip, take);
            return Ok(customersDTOs);
        }
    }
}
