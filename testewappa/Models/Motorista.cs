using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace testewappa.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }
    }
}