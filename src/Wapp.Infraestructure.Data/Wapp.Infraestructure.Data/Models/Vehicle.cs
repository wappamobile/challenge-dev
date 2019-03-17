using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.Infrastructure.Data.Models
{
    public class Vehicle
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public Vehicle(string marca, string modelo, string placa)
        {
            Marca = marca ?? throw new ArgumentNullException(nameof(marca));
            Modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
            Placa = placa ?? throw new ArgumentNullException(nameof(placa));
        }
        //public long DriverId { get; set; }


    }
}
