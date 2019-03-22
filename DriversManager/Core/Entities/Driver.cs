using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriversManager.Core.Entities
{
    public class Driver : IIdentityEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public long Id { get; set; }
        
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("LastName")]
        public string LastName { get; set; } 
        
        [BsonElement("CarTradeMark")]
        public string CarTradeMark { get; set; }
        
        [BsonElement("CarModel")]
        public string CarModel { get; set; }
        
        [BsonElement("CarLicense")]
        public string CarLicense { get; set; }
        
        [BsonElement("Street")]
        public string Street { get; set; }
        
        [BsonElement("Number")]
        public long Number { get; set; }
        
        [BsonElement("ZipCode")]
        public string ZipCode { get; set; }
        
        [BsonElement("City")]
        public string City { get; set; }
        
        [BsonElement("Estate")]
        public string Estate { get; set; }
        
        [BsonElement("Country")]
        public string Country { get; set; }
        
        [BsonElement("Lat")]
        public string Lat { get; set; }
        
        [BsonElement("Lng")]
        public string Lng { get; set; } 
    }
}
