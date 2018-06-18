using System;

namespace ApplicationCore.Entity
{
    public class Carro
    {
        public int CarroId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

    }
}
