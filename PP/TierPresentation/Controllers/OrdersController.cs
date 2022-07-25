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
        private readonly IPayService _paymentSevice;

        public OrdersController(IDataService dataService,
                                IAuthorizationService authorizationService,
                                IPayService paymentSevice)
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
        public async Task<IActionResult> Pay(int Id)
        {
            string sucsessURL = HttpContext.Request.Host + "/API/Orders/SucsessPay/" + Id.ToString();
            //Url.Action(
            //           "SucsessPay",
            //           "Orders") + ;

            string cancelURL = HttpContext.Request.Host + "/API/Orders/CancelPay/" + Id.ToString();

            //HttpContext.Request.Host + Url.Action(
            //           "CancelPay",
            //           "Orders") + Id.ToString();

           
            var canPay = await _paymentSevice.CanPayAsync(Id);

            if (canPay)
            {
                string serviceUrl = await _paymentSevice.StartPayAsync(Id, cancelURL, sucsessURL );
                if (serviceUrl.Length != 0)
                {
                    Response.Headers.Add("Location", serviceUrl);
                    return new StatusCodeResult(303);

                }
                else 
                {
                    return BadRequest("Order cant be payed");
                }

            }
            else
            {
                return BadRequest("Order cant be payed");
            }

        }

        [Authorize(Policy = "OnlyAuthenticated")]
        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Cancel(int Id)
        {
            return Ok();
        }
                
        [Route("{Id:int}")]
        public async Task<IActionResult> SucsessPay(int Id)
        {
            // check if request from  stripe service
            await _paymentSevice.PayFinishSuccessAsync(Id);
            return Ok("Sucsess pay");
        }
        
        [Route("{Id:int}")]
        public IActionResult CancelPay(int Id)
        {
            // check if request from  stripe service
            _paymentSevice.PayFinishFailAsync(Id);
            return Ok("Cancel pey");
        }
    }
}
