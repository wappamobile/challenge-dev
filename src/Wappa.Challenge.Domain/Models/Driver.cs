using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Wappa.Challenge.Domain.Models
{
    public class Driver
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Car Car { get; set; }
        public Address Address { get; set; }
    }
}