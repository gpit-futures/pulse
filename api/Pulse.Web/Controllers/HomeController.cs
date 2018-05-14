using Microsoft.AspNetCore.Mvc;

namespace Pulse.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return this.View();
        }
    }
}