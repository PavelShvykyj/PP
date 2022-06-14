using Microsoft.AspNetCore.Mvc;
using DTO.APIResourses;
using DTO.DTO;
using DataTier.Models;
using Microsoft.AspNetCore.JsonPatch;
using CoreTier.Interfaces;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodService _dataService;

        public GoodsController(IDataService dataService )
        {
            _dataService = dataService.GoodService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GoodResource goodResource)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            GoodDTO goodDTO = await _dataService.CreateAsync(goodResource);
            return Ok(goodDTO);
        }

        [HttpPost]
        [Route("{Id:int}/{Price:decimal}")]
        public IActionResult SetPrice(ushort id, decimal price)
        {
            _dataService.SetPrice(id, price);   
            return Ok();
        }

        [HttpPost]
        [Route("{Id:int}/{Rest:int}")]
        public IActionResult SetRest(ushort id, ushort rest)
        {
            _dataService.SetRest(id, rest);
            return Ok();
        }
        [HttpPost]
        public IActionResult SetRestToMany(GoodSetRestResouce[] goodResouces) 
        {
            _dataService.SetRestToMany(goodResouces.ToList());
            return Ok();
        }

        [HttpPost]
        public  IActionResult SetPriceToMany(GoodSetPriceResouce[] goodResouces)
        {
             _dataService.SetPriceToMany(goodResouces.ToList());
            return Ok();
        }

        [HttpPatch]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update(ushort id, JsonPatchDocument<Good> goodResource)
        {
            GoodDTO goodDTO = await _dataService.PatchAsync(id, goodResource, ModelState);
            if (!ModelState.IsValid) 
            {
                BadRequest(ModelState);
            }
            return Ok(goodDTO);
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            GoodDTO[] goodsDTOs = (GoodDTO[])_dataService.GetList(take, skip);
            return Ok(goodsDTOs);
        }
    }
}
