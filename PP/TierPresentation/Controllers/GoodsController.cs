using Microsoft.AspNetCore.Mvc;
using DTO.APIResourses;
using DTO.DTO;
using DataTier.Models;
using Microsoft.AspNetCore.JsonPatch;
using CoreTier.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Policy = "OnlyEmployee")]
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

        [Authorize(Policy = "OnlyEmployee")]
        [HttpPost]
        [Route("{Id:int}/{Price:decimal}")]
        public async Task<IActionResult> SetPrice(ushort id, decimal price)
        {
            var count = await _dataService.SetPriceAsync(id, price);   
            return Ok(string.Format("Saved {0} items", count));
        }

        [Authorize(Policy = "OnlyEmployee")]
        [HttpPost]
        [Route("{Id:int}/{Rest:int}")]
        public async Task<IActionResult> SetRest(ushort id, ushort rest)
        {
            var count = await _dataService.SetRestAsync(id, rest);
            return Ok(string.Format("Saved {0} items", count));
        }

        [Authorize(Policy = "OnlyEmployee")]
        [HttpPost]
        public async Task<IActionResult> SetRestToMany(GoodSetRestResouce[] goodResouces) 
        {
            var count = await _dataService.SetRestToManyAsync(goodResouces.ToList());
            return Ok(string.Format("Saved {0} items", count));
        }

        [Authorize(Policy = "OnlyEmployee")]
        [HttpPost]
        public async Task<IActionResult> SetPriceToMany(GoodSetPriceResouce[] goodResouces)
        {
            var count = await _dataService.SetPriceToManyAsync(goodResouces.ToList());
            return Ok(string.Format("Saved {0} items", count));
        }

        [Authorize(Policy = "OnlyEmployee")]
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

        [Authorize(Policy = "Onlyauthenticated")]
        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            GoodDTO[] goodsDTOs = (GoodDTO[])_dataService.GetList(take, skip);
            return Ok(goodsDTOs);
        }
    }
}
