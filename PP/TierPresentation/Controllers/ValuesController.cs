using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoreTier.APIResourses;
using DataTier;
using DataTier.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PP.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        readonly ApplicationContext _db;

        public ValuesController(IMapper mapper, ApplicationContext context)
        {
            _mapper = mapper;
            _db = context;
        }

        [HttpPost]
        [Route("{count:int}")]
        public async Task<IActionResult> SeedGoods(int count)
        {
            List<GoodResource> GoodsToAdd = new List<GoodResource>();
            
            for (int i = 0; i < count; i++)
            {
                GoodsToAdd.Add(new GoodResource { Name = "Good " + i.ToString() });
            }
            _db.Goods
                .AddRange(
                _mapper.Map<List<GoodResource>, List<Good>>(GoodsToAdd)
                .ToArray());

            await _db.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAllGoods()
        {
            _db.Goods.RemoveRange(_db.Goods.ToArray());
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("{count:int}")]
        public async Task<IActionResult> SeedCustomers(int count)
        {
            List<CustomerResource> CustomersToAdd = new List<CustomerResource>();

            for (int i = 0; i < count; i++)
            {
                CustomersToAdd.Add(new CustomerResource 
                { Name = "Customer " + i.ToString(),
                  Email = "Customer_" + i.ToString()+ "@.gmail.com"
                });
            }
            _db.Customers
                .AddRange(
                _mapper.Map<List<CustomerResource>, List<Customer>>(CustomersToAdd)
                .ToArray());

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            _db.Customers.RemoveRange(_db.Customers.ToArray());
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
