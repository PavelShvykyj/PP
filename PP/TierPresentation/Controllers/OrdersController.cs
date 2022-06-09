 using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP.APIResourses;
using PP.EF;
using PP.EF.Models;
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
            
            Order order = new Order();
            _mapper.Map<OrderSetResource, Order>(orderdata, order);
            order.Goods.Clear();

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            _= orderdata.Goods.Select(r => { r.OrderId = order.Id; return r; }).ToList();

            OrderRows[] orderGoods = _mapper.Map<OrderGoodsSetResource[], OrderRows[]>(orderdata.Goods.ToArray());
            _db.OrderRows.AddRange(orderGoods);
            await _db.SaveChangesAsync();

            order = _db.Orders.Include(c => c.Customer)
                              .Include(c => c.Goods)
                              .ThenInclude(r => r.Good)
                              .Single(o => o.Id == order.Id);

            return Ok(_mapper.Map<Order,OrderDTO>(order));
        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update(OrderSetResource orderdata, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = _db.Orders.Include(c => c.Customer)
                              .Include(c => c.Goods)
                              .ThenInclude(r => r.Good)
                              .SingleOrDefault(o => o.Id == id)!;

            if (order is null)
            {
                NotFound(new { Message = $"Item with Id {id} does not exist." });
            }
 
            _mapper.Map<OrderSetResource, Order>(orderdata, order);
            _db.Orders.Update(order);

            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<Order, OrderDTO>(order));
        }



        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            var res = _db.Orders
                            .Include(o => o.Customer)
                            .OrderBy(o=>o.CustomerId)
                            .Skip(skip)  
                            .Take(take)
                            .Select(o => new {id = o.Id, summ = o.Summ, customer = o.Customer  })
                            .ToList();


            return Ok(res);

            //return _af.GetResoult(RouteData, new { skip, take, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Pay()
        {
            return _af.GetResoult(RouteData, new { dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Cancel()
        {
            return _af.GetResoult(RouteData, new { dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

    }
}
