using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverRegistration.Domain.Entities
{
    public class Driver
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Car Car { get; set; }
        public Address Address { get; set; }

        public string FullAddress 
        {
            get
            {
                return Address?.Street + ", "
                     + Address?.Number + ", "
                     + Address?.City + ", "
                     + Address?.State;
            }
        }
    }
}
