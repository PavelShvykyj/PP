using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PP.Fake;

namespace PP.Controllers
{
    [Route("API/[Controller]/[Action]")]
    public class GoodsController : Controller
    {
        private ActionsResultFake _af;

        public GoodsController(ActionsResultFake af)
        {
            _af = af;
        }

        [HttpPost]
        public IActionResult Create()
        {

            return Json("Ok Create");
        }

        [HttpPost]
        [Route("{id:int}/{price:float}")]
        public IActionResult SetPrice(int id, float price)
        {

             return _af.GetResoult(RouteData, new { id,  price , dev = ControllerContext.HttpContext.Items["IsDevelopment"] } );

        }

        [HttpPost]
        [Route("{id:int}/{rest:int}")]
        public IActionResult SetRest(int id, int rest)
        {

            return _af.GetResoult(RouteData, new { id,  rest, dev = ControllerContext.HttpContext.Items["IsDevelopment"] } );
        }

        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Update()
        {
            return _af.GetResoult(RouteData, new {  dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            return _af.GetResoult(RouteData, new { skip, take, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
        }


    }
}
