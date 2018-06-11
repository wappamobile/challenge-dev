using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WebApi_challengedev.Data
{
    public class Motoristas
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public Carro DadosCarro = new Carro();
        public Endereco DadosEndereco = new Endereco();

    }

    public class Carro
    {
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
    }

    public class Endereco
    {
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public int? Numero { get; set; }
        public string Lat { get; set; }
        public string Log { get; set; }

    }
}
