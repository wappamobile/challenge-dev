using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MG.WappaDriverAPI.Core.Models
{
    public class Driver
    {
        public ObjectId Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Car")]
        public Car Car { get; set; }

        [BsonIgnore]
        public IEnumerable<Address> Addresses { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("ModifiedAt")]
        public DateTime? ModifiedAt { get; set; }
    }
}
