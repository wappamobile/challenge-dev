namespace WappaMobile.Driver.API.Model
{
    public class Geometry
    {
        public Coordinate Bounds { get; set; }

        public Location Location { get; set; }

        public string LocationType { get; set; }

        public Coordinate Viewport { get; set; }
    }
}
