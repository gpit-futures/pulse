namespace Pulse.Web.Controllers.Search.ResponseModels
{
    public class HeadlineEntry
    {
        public string Source { get; set; }

        public string SourceId { get; set; }

        public object LatestEntry { get; set; }

        public int TotalEntries { get; set; }
    }
}