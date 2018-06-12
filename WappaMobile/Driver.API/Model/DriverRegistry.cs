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

        public DriverName Name { get; set; }

        public DriverVehicle Vehicle { get; set; }

        public string Address { get; set; }
    }
}
