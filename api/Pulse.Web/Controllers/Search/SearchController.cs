using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulse.Web.Controllers.Search.RequestModels;
using Pulse.Web.Controllers.Search.ResponseModels;

namespace Pulse.Web.Controllers.Search
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        [HttpPost("patient/table")]
        public async Task<IActionResult> MainSearch([FromBody] MainSearchRequest request)
        {
            var result = new MainSearchResponse();
            return this.Ok(result);
        }
    }
}