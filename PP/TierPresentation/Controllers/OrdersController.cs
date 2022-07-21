using Microsoft.AspNetCore.Mvc;
using DTO.APIResourses;
using DTO.DTO;
using CoreTier.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CoreTier.Services;

namespace PP.Controllers
{
    [ApiController]
    [Route("API/[Controller]/[Action]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _dataService;
        private readonly IAuthorizationService _authorizationService;
        private readonly PaymentSevice _paymentSevice;

        public OrdersController(IDataService dataService,
                                IAuthorizationService authorizationService,
                                PaymentSevice paymentSevice)
        {
            _dataService = dataService.OrderService;
            _authorizationService = authorizationService;
            _paymentSevice = paymentSevice;    
        }

        [Authorize(Policy = "OnlyAuthenticated")]
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

        [Authorize(Policy = "OnlyAuthenticated")]
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
        
        [HttpGet]
        public IActionResult TestPay()
        {
            string sucsessURL = HttpContext.Request.Host + Url.Action(
                       "SucsessPay",
                       "Orders");

            string cancelURL = HttpContext.Request.Host+Url.Action(
                       "CancelPay",
                       "Orders");

            string sessionURL = _paymentSevice.CreateSession(sucsessURL, cancelURL);
            Response.Headers.Add("Location", sessionURL);
            return new StatusCodeResult(303);

        }

        public IActionResult SucsessPay()
        {
            _paymentSevice.RefreshSession();
            return Ok("Sucsess pay "+ _paymentSevice._session.PaymentIntent.Status);
        }

        public IActionResult CancelPay()
        {
            return Ok("Cancel pey");
        }


    }
}
