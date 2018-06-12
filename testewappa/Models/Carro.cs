using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace testewappa.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}