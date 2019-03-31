using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MG.WappaDriverAPI.Core.Models
{
    public class Address
    {
        [BsonElement("ZipOrPostcode")]
        public string ZipOrPostcode  { get; set; }

        [BsonElement("StreetOrAddress")]
        public string StreetOrAddress { get; set; }

        [BsonElement("SuiteOrApartment")]
        public string SuiteOrApartment { get; set; }

        [BsonElement("City")]
        public string City { get; set; }

        [BsonElement("StateOrProvince")]
        public string StateOrProvince { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        
        public ObjectId Id { get; set; }

        [BsonElement("DriverId")]
        public string DriverId { get; set; }

        [BsonElement("StreetNumber")]
        public int StreetNumber { get; set; }

        [BsonElement("Longitude")]
        public double Longitude { get; set; }

        [BsonElement("Latitude")]
        public double Latitude { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("ModifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [BsonElement("Neighborhood")]
        public string Neighborhood { get; set; }

    }
}
