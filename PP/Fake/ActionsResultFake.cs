using Microsoft.AspNetCore.Mvc;

namespace PP.Fake
{

    

    [NonController]
    public  class ActionsResultFake : Controller    {
        
        public IActionResult GetResoult(RouteData data, object args = null) {
            return Json(new { par = data.Values, args = args }); ;
        }

    }
}
