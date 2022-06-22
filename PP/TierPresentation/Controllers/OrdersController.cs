using Microsoft.AspNetCore.Mvc;
using DTO.APIResourses;
using DTO.DTO;
using CoreTier.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _dataService;
        private readonly IAuthorizationService _authorizationService;

        public OrdersController(IDataService dataService, IAuthorizationService authorizationService)
        {
            _dataService = dataService.OrderService;
            _authorizationService = authorizationService;
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(OrderSetResource orderdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resoult = await _authorizationService.AuthorizeAsync(
                HttpContext.User,
                orderdata,
                "OwnOrders");

            if (!resoult.Succeeded)
            {
                return BadRequest("Only own orders can be created");
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

            var resoult = await _authorizationService.AuthorizeAsync(
                                HttpContext.User,
                                orderdata,
                                "OwnOrders");

            if (!resoult.Succeeded)
            {
                return BadRequest("Only own orders can be created");
            }



            OrderDTO orderDTO = await _dataService.UpdateAsync(id,orderdata);
            return Ok(orderDTO);
        }

        [Authorize(Policy = "OnlyAuthenticated")]
        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            var orderDTOs = _dataService.GetList(take, skip);


            return Ok(orderDTOs);
        }

        [Authorize(Policy = "OnlyAuthenticated")]
        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Pay()
        {
            return Ok();
        }

        [Authorize(Policy = "OnlyAuthenticated")]
        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Cancel()
        {
            return Ok();
        }
    }
}
