using MongoDB.Bson.Serialization.Attributes;

namespace MG.WappaDriverAPI.Core.Models
{
    public class Car
    {
        [BsonElement("CarPlate")]
        public string CarPlate { get; set; }

        [BsonElement("CarColor")]
        public string CarColor { get; set; }

        [BsonElement("Brand")]
        public string Brand { get; set; }

        [BsonElement("Model")]
        public string Model { get; set; }
    }
}