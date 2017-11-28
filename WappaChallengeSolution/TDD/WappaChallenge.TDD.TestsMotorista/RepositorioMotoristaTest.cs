using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.Repositorio.Databases;
using WappaChallenge.Repositorio.Repositorios;

namespace TDD.TestsMotorista
{
    [TestClass]
    public class RepositorioMotoristaTest
    {
        private IMotoristaRepositorio _repositorio;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repositorio = new MotoristaRepositorio(new MockStaticDatabase());

            for (int i = 0; i < 10; i++)
            {
                this._repositorio.Cadastrar(new Motorista(
                            primeiroNome: "Andrey",
                            ultimoNome: "Frazatti",
                            veiculo: new Veiculo(
                                marca: "Chevrolet",
                                modelo: "Ônix LT 1.0 2017",
                                placa: "QIV-2872"),
                            endereco: new Endereco(
                                logradouro: "Avenida Paraguaçu Paulista",
                                numero: "287 B",
                                complemento: "Casa 3",
                                bairro: "Jardim Ana Estela",
                                cidade: "Carapicuíba",
                                estado: "São Paulo",
                                cep: "06364-550",
                                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F))));
            }
        }
    }
}
