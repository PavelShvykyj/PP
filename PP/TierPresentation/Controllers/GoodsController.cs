using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PP.Fake;
using AutoMapper;
using DataTier;
using PP.CoreTier.APIResourses;
using DataTier.Models;
using Microsoft.AspNetCore.JsonPatch;

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
        public async Task<IActionResult> Create(GoodResource goodResource)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            Good newGood = _mapper.Map<GoodResource, Good>(goodResource);
            _db.Goods.Add(newGood);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<Good, GoodDTO>(newGood));
        }

        [HttpPost]
        [Route("{Id:int}/{Price:decimal}")]
        public async Task<IActionResult> SetPrice(ushort id, decimal price)
        {
            var Good = _db.Goods.SingleOrDefault(g => g.Id == id);
            if (Good is null)
            {
                return BadRequest();
            }
            Good.Price = price;
            await _db.SaveChangesAsync();
            return Ok();
            //return _af.GetResoult(RouteData, new { Id,  Price , dev = ControllerContext.HttpContext.Items["IsDevelopment"] } );
        }

        [HttpPost]
        [Route("{Id:int}/{Rest:int}")]
        public async Task<IActionResult> SetRest(ushort id, ushort rest)
        {
            var Good = _db.Goods.SingleOrDefault(g => g.Id == id);
            if (Good is null)
            {
                return BadRequest();
            }
            Good.Rest = rest;
            await _db.SaveChangesAsync();
            return Ok();
            //return _af.GetResoult(RouteData, new { Id,  Rest, dev = ControllerContext.HttpContext.Items["IsDevelopment"] } );
        }
        [HttpPost]
        public async Task<IActionResult> SetRestToMany(GoodSetRestResouce[] goodResouces) 
        {
            Good[] goods = _mapper.Map<GoodSetRestResouce[], Good[]>(goodResouces);
            foreach (Good item in goods)
            {
                _db.Goods.Attach(item).Property(g=>g.Rest).IsModified = true;
            }
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SetPriceToMany(GoodSetPriceResouce[] goodResouces)
        {
            Good[] goods = _mapper.Map<GoodSetPriceResouce[], Good[]>(goodResouces);
            foreach (Good item in goods)
            {
                _db.Goods.Attach(item).Property(g => g.Price).IsModified = true;
            }
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update(ushort id, JsonPatchDocument<Good> goodResource)
        {
            if (goodResource == null)
            {
                return BadRequest("No data to patch");
            }


            var Good = _db.Goods.SingleOrDefault(g => g.Id == id);
            
            if (Good is null)
            {
                NotFound(new { Message = $"Item with Id {id} does not exist." });
            }

            goodResource.ApplyTo(Good, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_mapper.Map<GoodResource, Good>(GoodResource, Good);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<Good, GoodDTO>( Good));
            //return _af.GetResoult(RouteData, new {  dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            Good[] goods = _db.Goods
                .Skip(skip)
                .Take(take)
                .ToArray();
            GoodDTO[] goodsDTOs = _mapper.Map<Good[], GoodDTO[]>(goods);
            return Ok(goods);

            //return _af.GetResoult(RouteData, new { skip, take, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }
    }
}
