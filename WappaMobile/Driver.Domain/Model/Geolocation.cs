namespace WappaMobile.Driver.API.Model
{
    public class Geolocation
    {
        //Field 'address_components' omitted.

        public string FormattedAddress { get; set; }

        public Geometry Geometry { get; set; }

        public string PlaceId { get; set; }
    }
}
