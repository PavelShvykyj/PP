using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP.API_Resourses;
using PP.EF;
using PP.EF.models;
using PP.Fake;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class OrdersController : ControllerBase
    {
        private ActionsResultFake _af;
        private readonly IMapper _mapper;
        readonly ApplicationContext _db;

        public OrdersController(ActionsResultFake af, IMapper mapper, ApplicationContext context)
        {
            _af = af;
            _mapper = mapper;
            _db = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderSetResource orderdata)
        {
         
            /// check for existing entities by foreing keys
            
            Orders Order = new Orders();
            _mapper.Map<OrderSetResource, Orders>(orderdata, Order);
            Order.rows.Clear();

            _db.Orders.Add(Order);
            await _db.SaveChangesAsync();

            _= orderdata.rows.Select(r => { r.orderid = Order.id; return r; }).ToList();

            OrderRows[] orows = _mapper.Map<OrderRowsSetResource[], OrderRows[]>(orderdata.rows.ToArray());
            _db.OrderRows.AddRange(orows);
            await _db.SaveChangesAsync();

            Order = _db.Orders.Include(c => c.customer)
                              .Include(c => c.rows)
                              .ThenInclude(r => r.good)
                              .Single(o => o.id == Order.id);

            return Ok(_mapper.Map<Orders,OrdersDTO>(Order));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(OrderSetResource orderdata, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Orders Order = _db.Orders.Include(c => c.customer)
                              .Include(c => c.rows)
                              .ThenInclude(r => r.good)
                              .SingleOrDefault(o => o.id == id)!;

            if (Order is null)
            {
                NotFound(new { Message = $"Item with id {id} does not exist." });
            }
 
            _mapper.Map<OrderSetResource, Orders>(orderdata, Order);
            _db.Orders.Update(Order);

            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<Orders, OrdersDTO>(Order));
        }



        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            var res = _db.Orders
                            .Include(o => o.customer)
                            .OrderBy(o=>o.customerid)
                            .Skip(skip)  
                            .Take(take)
                            .Select(o => new {id = o.id, summ = o.summ, customer = o.customer  })
                            .ToList();


            return Ok(res);

            //return _af.GetResoult(RouteData, new { skip, take, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Pay()
        {
            return _af.GetResoult(RouteData, new { dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Cancel()
        {
            return _af.GetResoult(RouteData, new { dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

    }
}
