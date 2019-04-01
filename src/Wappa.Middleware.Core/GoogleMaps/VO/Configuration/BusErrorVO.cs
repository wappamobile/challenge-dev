namespace Wappa.Middleware.Core.GoogleMaps.VO.Configuration
{
    public partial class BusErrorVO
    {
        public string Message { get; set; }
        public object[] ValidationErrors { get; set; }
        public object Detail { get; set; }
    }
}
