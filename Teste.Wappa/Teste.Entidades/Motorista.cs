using System;
using System.Collections.Generic;
using System.Text;
using Teste.Entidades.Extensoes;

namespace Teste.Entidades
{ 
    public class NomeCompleto
    {
        public NomeCompleto(string primeiroNome, string ultimoNome)
        {
            this.PrimeiroNome = primeiroNome.ValidaAtributo(nameof(primeiroNome));
            this.UltimoNome = ultimoNome.ValidaAtributo(nameof(ultimoNome));
        }
        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
    }

    public class Carro
    {
        public Carro(string marca, string modelo, string placa)
        {
            this.Marca = marca.ValidaAtributo(nameof(marca)); 
            this.Modelo = modelo.ValidaAtributo(nameof(modelo));
            this.Placa = placa.ValidaAtributo(nameof(placa));
        }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
    }

    public class Endereco
    {
        public Endereco(string logradouro, short numero, string bairro, string cidade, string estado, int cep)
        {
            this.Logradouro = logradouro.ValidaAtributo(nameof(logradouro));
            this.Numero = numero.ValidaAtributo(nameof(numero));
            this.Bairro = bairro.ValidaAtributo(nameof(bairro));
            this.Cidade = cidade.ValidaAtributo(nameof(cidade));
            this.Estado = estado.ValidaAtributo(nameof(estado));
            this.CEP = cep.ValidaAtributo(nameof(cep));
        }

        public void InformarCoordenadas(double latitude, double longitude)
        {
            this.Coordenadas = new Coordenadas(latitude.ValidaAtributo(nameof(latitude)), longitude.ValidaAtributo(nameof(longitude)));
        }

        public void InformarCoordenadas(Coordenadas coordenadas)
        {
            this.Coordenadas = coordenadas.ValidaAtributo(nameof(coordenadas));
        }

        public string Logradouro { get; private set; }
        public short Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public int CEP { get; private set; }
        public Coordenadas Coordenadas { get; private set; }
    }

    public class Motorista
    {
        public Motorista(string primeiroNome, string ultimoNome)
        {
            this.Nome = new NomeCompleto(primeiroNome, ultimoNome);
        }

        public void InformarCarro(string marca, string modelo, string placa)
        {
            this.Carro = new Carro(marca, modelo, placa);
        }

        public void InformarEndereco(string logradouro, short numero, string bairro, string cidade, string estado, int cep) {
            this.Endereco = new Endereco(logradouro, numero, bairro, cidade, estado, cep);
        }

        public NomeCompleto Nome { get; private set; }
        public Carro Carro { get; private set; }
        public Endereco Endereco { get; private set; }
    }
}
