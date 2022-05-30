using Microsoft.AspNetCore.Mvc;
using PP.Fake;

namespace PP.Controllers
{
    [Route("API/[Controller]/[Action]")]
    public class OrdersController : Controller
    {
        private ActionsResultFake _af;

        public OrdersController(ActionsResultFake af)
        {
            _af = af;
        }


        [HttpPost]
        public IActionResult Create()
        {
            return Json("Ok Create");
        }


        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            return _af.GetResoult(RouteData, new { skip, take, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
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
