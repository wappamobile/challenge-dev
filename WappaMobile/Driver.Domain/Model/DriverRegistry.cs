using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WappaMobile.Driver.API.Model
{
    public class DriverRegistry
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public FullName Name { get; set; }

        public Vehicle Vehicle { get; set; }

        public string Address { get; set; }

        public Geolocation Geolocation { get; set; }

        public bool FetchGeolocation { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
