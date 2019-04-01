namespace Wappa.Middleware.Core.GoogleMaps.VO.Configuration
{
    public class BusResultWrapper<TResp>
    {
        public object TargetUrl { get; set; }

        public bool Success { get; set; }

        public TResp Result { get; set; }

        public BusErrorVO Error { get; set; }

        public bool UnAuthorizedRequest { get; set; }
    }
}
