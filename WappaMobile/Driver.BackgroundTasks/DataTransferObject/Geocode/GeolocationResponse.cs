using System.Collections.Generic;
using Newtonsoft.Json;

namespace WappaMobile.Driver.BackgroundTasks.DataTransferObject.Geocode
{
    public class GeolocationResponse
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        //public List<API.Model.Geolocation> ToModel()
        //{
        //    var list = new List<API.Model.Geolocation>();

        //    foreach (var result in Results)
        //    {
        //        var item = new API.Model.Geolocation();

        //        item.Geometry = new API.Model.Geometry();

        //        item.Geometry.Bounds = new API.Model.Coordinate();
        //        item.Geometry.Bounds.Northeast = new API.Model.Location();
        //        item.Geometry.Bounds.Northeast.Latitude = result.Geometry.Bounds.Northeast.Latitude;
        //        item.Geometry.Bounds.Northeast.Longitude = result.Geometry.Bounds.Northeast.Longitude;
        //        item.Geometry.Bounds.Southwest = new API.Model.Location();
        //        item.Geometry.Bounds.Southwest.Latitude = result.Geometry.Bounds.Southwest.Latitude;
        //        item.Geometry.Bounds.Southwest.Longitude = result.Geometry.Bounds.Southwest.Longitude;

        //        item.Geometry.Viewport = new API.Model.Coordinate();
        //        item.Geometry.Viewport.Northeast = new API.Model.Location();
        //        item.Geometry.Viewport.Northeast.Latitude = result.Geometry.Viewport.Northeast.Latitude;
        //        item.Geometry.Viewport.Northeast.Longitude = result.Geometry.Viewport.Northeast.Longitude;
        //        item.Geometry.Viewport.Southwest = new API.Model.Location();
        //        item.Geometry.Viewport.Southwest.Latitude = result.Geometry.Viewport.Southwest.Latitude;
        //        item.Geometry.Viewport.Southwest.Longitude = result.Geometry.Viewport.Southwest.Longitude;

        //        item.Geometry.Location = new API.Model.Location();
        //        item.Geometry.Location.Latitude = result.Geometry.Location.Latitude;
        //        item.Geometry.Location.Longitude = result.Geometry.Location.Longitude;

        //        item.Geometry.LocationType = result.Geometry.LocationType;
        //        item.FormattedAddress = result.FormattedAddress;
        //        item.PlaceId = result.PlaceId;

        //        list.Add(item);
        //    }

        //    return list;
        //}
    }
}
