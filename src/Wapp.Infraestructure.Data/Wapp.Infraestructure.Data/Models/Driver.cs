using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Wappa.Infrastructure.Data.Models
{
    public class Driver
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public string Address { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }

        public Driver(string id,
                      string firstName,
                      string lastName,
                      string address,
                      double latitude,
                      double longitude,
                      Vehicle vehicle)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Latitude = latitude;
            Longitude = longitude;
            Vehicle = vehicle;
        }
    }
}
