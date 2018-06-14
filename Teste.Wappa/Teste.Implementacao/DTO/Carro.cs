using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.Inteface;
using Teste.Repositorio.DTO.Extensoes;

namespace Teste.Implementacao.DTO
{
    public class Carro : IDTO
    {
        public Carro(int? id, int? idMarca, string marca, int? idModelo, string modelo, string placa)
        {
            this.Id = id;
            this.IdMarca = idMarca;
            this.Marca = marca;
            this.IdModelo = idModelo.ValidaAtributo(nameof(idModelo));
            this.Modelo = modelo;
            this.Placa = placa.ValidaAtributo(nameof(placa));
        }

        public Carro(int? id, int idModelo, string placa) : 
            this(id, null, null, idModelo, null, placa) { }

        public Carro(int idModelo, string placa) : this(null, null, null, idModelo, null, placa) { } 

        public int? Id { get; private set; }
        public int? IdMarca { get; private set; }
        public string Marca { get; private set; }
        public int? IdModelo { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
    }
}
