using Microsoft.AspNetCore.Mvc;

namespace POC.API
{
    [Route("")]
    public class HomeController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return new RedirectResult("/swagger");
        }
    }
}