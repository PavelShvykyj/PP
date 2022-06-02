using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PP.Fake;
using AutoMapper;
using PP.EF;
using PP.API_Resourses;
using PP.EF.models;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class GoodsController : ControllerBase
    {
        private readonly ActionsResultFake _af;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;

        public GoodsController(ActionsResultFake af, IMapper mapper, ApplicationContext context)
        {
            _af = af;
            _mapper = mapper;
            _db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GoodResource GoodResource)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            Goods newGood = _mapper.Map<GoodResource, Goods>(GoodResource);
            _db.Goods.Add(newGood);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<Goods, GoodsDTO>(newGood));
        }

        [HttpPost]
        [Route("{id:int}/{price:decimal}")]
        public async Task<IActionResult> SetPrice(ushort id, decimal price)
        {
            var Good = _db.Goods.SingleOrDefault(g => g.id == id);
            if (Good is null)
            {
                return BadRequest();
            }
            Good.price = price;
            await _db.SaveChangesAsync();
            return Ok();
            //return _af.GetResoult(RouteData, new { id,  price , dev = ControllerContext.HttpContext.Items["IsDevelopment"] } );
        }

        [HttpPost]
        [Route("{id:int}/{rest:int}")]
        public async Task<IActionResult> SetRest(ushort id, ushort rest)
        {
            var Good = _db.Goods.SingleOrDefault(g => g.id == id);
            if (Good is null)
            {
                return BadRequest();
            }
            Good.rest = rest;
            await _db.SaveChangesAsync();
            return Ok();

            //return _af.GetResoult(RouteData, new { id,  rest, dev = ControllerContext.HttpContext.Items["IsDevelopment"] } );
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(ushort id, GoodResource GoodResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Good = _db.Goods.SingleOrDefault(g => g.id == id);
            if (Good is null)
            {
                return BadRequest();
            }
            _mapper.Map<GoodResource, Goods>(GoodResource, Good);
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<Goods, GoodsDTO>( Good));
            //return _af.GetResoult(RouteData, new {  dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            return _af.GetResoult(RouteData, new { skip, take, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }


    }
}
