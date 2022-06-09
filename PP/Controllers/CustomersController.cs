using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PP.API_Resourses;
using PP.EF;
using PP.EF.models;
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

            if (_db.Customers.Where(c => c.email == CustomerResource.email).Count() != 0) 
            {
                return Ok("Exists already");
            }

            Customers newCustomer = _mapper.Map<CustomerResource, Customers>(CustomerResource);
            _db.Customers.Add(newCustomer);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<Customers, CustomersDTO>(newCustomer));
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, CustomerResource CustomerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Customer = _db.Customers.SingleOrDefault(g => g.id == id);
            if (Customer is null)
            {
                return BadRequest();
            }
            _mapper.Map<CustomerResource, Customers>(CustomerResource, Customer);
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<Customers, CustomersDTO>(Customer));
            //return _af.GetResoult(RouteData);
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            Customers[] customers =  _db.Customers
                .OrderBy(c=> c.email)
                .Skip(skip)
                .Take(take)
                .ToArray();
            
            CustomersDTO[] customersDTOs = _mapper.Map<Customers[], CustomersDTO[]>(customers);
            
            return Ok(customersDTOs);
            //return _af.GetResoult(RouteData, new {  take,  skip, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }
    }
}
