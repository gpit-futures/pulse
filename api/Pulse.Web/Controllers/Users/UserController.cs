using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Pulse.Web.Controllers.Users
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult GetDefaultUser()
        {
            return this.Ok(new
            {
                email = "alex.mcnair@gmail.com",
                family_name = "McNair",
                given_name = "Alex",
                role = "IDCR",
                roles = new List<string> { "IDCR" },
                sub = "28AD8576-1948-4C84-8B5E-55FB7EE027CE",
                tenant = null as object
            });
        }
    }
}