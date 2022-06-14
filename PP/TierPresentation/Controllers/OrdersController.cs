using Microsoft.AspNetCore.Mvc;
using DTO.APIResourses;
using DTO.DTO;
using CoreTier.Interfaces;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _dataService;

        public OrdersController(IDataService dataService)
        {
            _dataService = dataService.OrderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderSetResource orderdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderDTO orderDTO = await _dataService.CreateAsync(orderdata);
            return Ok(orderDTO);
        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update(OrderSetResource orderdata, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrderDTO orderDTO = await _dataService.UpdateAsync(id,orderdata);
            return Ok(orderDTO);
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            var orderDTOs = _dataService.GetList(take, skip);
            return Ok(orderDTOs);
        }

        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Pay()
        {
            return Ok();
        }

        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Cancel()
        {
            return Ok();
        }
    }
}
