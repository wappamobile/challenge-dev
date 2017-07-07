using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Wappa.Domain.Entities;
using Wappa.Domain.Concrete;

namespace Wappa.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            MarcaRepository x = new MarcaRepository();
            Marca marca = new Marca() { Descricao = "Volkswagen" };
            x.add(marca);
            int id = marca.ID;
            Console.Write(id);
        }

        [Fact]
        public void Test2() {
            ModeloRepository x = new ModeloRepository();
            Modelo modelo = new Modelo() { Marca =  new Marca() {ID = 1}, Descricao = "Gol" };
            x.add(modelo);
            int id = modelo.ID;
            Console.Write(id);
        }

        [Fact]
        public void Test3() {
            CarroRepository x = new CarroRepository();
            Carro carro = new Carro() { Modelo =  new Modelo() {ID = 1}, Placa = "ABC1234" };
            x.add(carro);
            int id = carro.ID;
            Console.Write(id);
        }

        [Fact]
        public void Test4() {
            EnderecoRepository x = new EnderecoRepository();
            Endereco endereco = new Endereco() { 
                CEP="04548-000"
                , Logradouro="Av. Dr. Cardoso de Melo"
                , Numero=987, Complemento="3o. Andar"
                , Bairro="Vila Olimpia", Cidade="São Paulo"
                , Estado = "SP"
                , Latitude=-23.5992753, Longitude=-46.6823935 };
            x.add(endereco);
            int id = endereco.ID;
            Console.Write(id);
        }        

        [Fact]
        public void Test5() {
            MotoristaRepository x = new MotoristaRepository();
            Motorista motorista = new Motorista() { 
                PrimeiroNome="Francisco"
                , UltimoNome = "Costa de Abreu"
                , Endereco = new Endereco { ID = 4 }
                , Carro = new Carro {ID = 1}
             };
            x.add(motorista);
            int id = motorista.ID;
            Console.Write(id);
        }                
    }
}
