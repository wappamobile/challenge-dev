using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Domain.DriverAggregation
{
    public class Driver
    {
        private Driver()
        {

        }

        public Driver(AddDriverDto dto, ICordinatesService coodinatesService)
        {
            Id = ObjectId.GenerateNewId();
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Car = dto.Car;
            Address = dto.Address;
            
        }
        [BsonId]
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Car Car { get; set; }
        public string Address { get; set; }

        public void Update(UpdateDriverDto dto, ICordinatesService cordinatesService)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Car = dto.Car;
            Address = dto.Address;
        }
    }
}
