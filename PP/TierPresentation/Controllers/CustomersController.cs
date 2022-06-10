using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PP.CoreTier.APIResourses;
using PP.CoreTier.DTO;
using DataTier;
using DataTier.Models;
using PP.Fake;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class CustomersController : ControllerBase
    {
        private ActionsResultFake _af;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;
        public CustomersController(ActionsResultFake af , IMapper mapper, ApplicationContext context)
        {
            _af = af;
            _mapper = mapper;
            _db = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CustomerResource CustomerResource)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest();
            }

            if (_db.Customers.Where(c => c.Email == CustomerResource.Email).Count() != 0) 
            {
                return Ok("Exists already");
            }

            Customer newCustomer = _mapper.Map<CustomerResource, Customer>(CustomerResource);
            _db.Customers.Add(newCustomer);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<Customer, CustomerDTO>(newCustomer));
        }

        [HttpPost]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update(int id, CustomerResource CustomerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Customer = _db.Customers.SingleOrDefault(g => g.Id == id);
            if (Customer is null)
            {
                return BadRequest();
            }
            _mapper.Map<CustomerResource, Customer>(CustomerResource, Customer);
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<Customer, CustomerDTO>(Customer));
            //return _af.GetResoult(RouteData);
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            Customer[] customers =  _db.Customers
                .OrderBy(c=> c.Email)
                .Skip(skip)
                .Take(take)
                .ToArray();
            
            CustomerDTO[] customersDTOs = _mapper.Map<Customer[], CustomerDTO[]>(customers);
            
            return Ok(customersDTOs);
            //return _af.GetResoult(RouteData, new {  take,  skip, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }
    }
}
