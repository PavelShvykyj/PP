using Microsoft.AspNetCore.Mvc;
using PP.Fake;

namespace PP.Controllers
{
    [Route("API/[Controller]/[Action]")]
    public class CustomersController : Controller
    {
        private ActionsResultFake _af;

        public CustomersController(ActionsResultFake af)
        {
            _af = af;
        }
        
        [HttpPost]
        public IActionResult Create()
        {

            return Json("Ok Create");
        }

        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Update(int id)
        {
            return _af.GetResoult(RouteData);
        }

        [HttpGet]
        [Route("{skip:int}/{take:int:max(100)}")]
        public IActionResult GetList(int skip, int take)
        {
            return _af.GetResoult(RouteData, new {  take,  skip, dev = ControllerContext.HttpContext.Items["IsDevelopment"] });
            
        }



    }
}
