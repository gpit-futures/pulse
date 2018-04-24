using Microsoft.AspNetCore.Mvc;

namespace Pulse.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/initialise")]
    public class InitController : Controller
    {
        [HttpGet]
        public IActionResult Initialise()
        {
            return this.Ok(new
            {
                mode = "demo",
                ok = true,
                version = "2.32.2"
            });
        }
    }
}