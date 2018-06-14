using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Teste.Implementacao;
using Teste.Implementacao.Filtro;
using System.Linq;
using Teste.Implementacao.DTO;
using Teste.Repositorio.Interface;

namespace Teste.TesteUnitario
{

    [TestClass]
    public class CadastroDeMotoristaTeste
    {
        private ICadastro implementacao;
        private IRepositorio<Repositorio.Entidade.Motorista> repositorio;

        [TestInitialize]
        public void Inicio() {
            implementacao = new CadastroDeMotorista();
            repositorio = Substitute.For<IRepositorio<Repositorio.Entidade.Motorista>>();            
        }

        private void MockarRepositorio()
        {
            ((CadastroDeMotorista)implementacao).Repositorio = repositorio;
        }

        private void MockarConsulta(Repositorio.Entidade.Motorista motorista){
            repositorio.Inserir(motorista);
        }

        [TestMethod]
        public void CadastrouMotoristaComSucesso()
        {
            var motorista = MotoristaStub.ObterMotoristaParaCadastroValido();
            var validaExecucao = false;

            repositorio
                .WhenForAnyArgs(r => r.Inserir(MotoristaStub.ObterRepositorioMotoristaParaCadastroValido()))
                .Do(r => {
                    var motoristaEntrada = (Repositorio.Entidade.Motorista)r[0];
                    if (motoristaEntrada.Endereco.Latitude == MotoristaStub.LatitudeCadastro && 
                        motoristaEntrada.Endereco.Longitude == MotoristaStub.LongitudeCadastro)
                        validaExecucao = true;
                });
                
            MockarRepositorio();

            implementacao.Cadastrar(motorista);

            Assert.IsTrue(validaExecucao, "Não foi realizado o cadastro com sucesso");
        }

        [TestMethod]
        public void ConsultouRegistrosOrdenadosPorNome() {
            var filtro = new FiltroMotorista();
            filtro.OrdenarMotoristaPor = OrdenacaoListaMotorista.Sobrenome;

            repositorio.Consultar().Returns(MotoristaStub.ObterRepositorioListaMotoristasSemOrdenar());

            MockarRepositorio();

            var listaMotoristas = implementacao.Consultar(filtro);

            Assert.AreEqual(3, listaMotoristas.Count());

            var retorno = listaMotoristas.ToArray();
            Assert.AreEqual("Antunes", ((Motorista)retorno[0]).Sobrenome);
            Assert.AreEqual("Oliveira", ((Motorista)retorno[1]).Sobrenome);
            Assert.AreEqual("Silva", ((Motorista)retorno[2]).Sobrenome);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidouSobrenomeMotorista()
        {
            var motorista = MotoristaStub.ObterMotoristaParaCadastroSobrenomeInvalido();

            implementacao.Cadastrar(motorista);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidouLogradouroEndereco()
        {
            var motorista = MotoristaStub.ObterMotoristaParaCadastroLogradouroInvalido();

            implementacao.Cadastrar(motorista);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidouPlacaCarro()
        {
            var motorista = MotoristaStub.ObterMotoristaParaCadastroPlacaInvalida();

            implementacao.Cadastrar(motorista);
        }
        
    }
}
