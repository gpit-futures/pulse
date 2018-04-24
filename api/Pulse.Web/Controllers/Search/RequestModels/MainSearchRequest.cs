namespace Pulse.Web.Controllers.Search.RequestModels
{
    public class MainSearchRequest
    {
        public string OrderType { get; set; }

        public int PageNumber { get; set; }

        public string SearchString { get; set; }
    }
}
