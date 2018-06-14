using System;
using System.Collections.Generic;
using System.Linq;
using Teste.Implementacao.Builder;
using Teste.Implementacao.DTO;
using Teste.Implementacao.Filtro;
using Teste.Implementacao.Inteface;
using Teste.Implementacao.Transformacao;
using Teste.Repositorio.Interface;
using Teste.Repositorio.Repositorio;
using Teste.Servicos.Externos.DTO;

namespace Teste.Implementacao
{
    public class CadastroDeMotorista : Cadastro<Motorista>
    {
        public CadastroDeMotorista() { }
        
        public override void Cadastrar(IDTO dadosCadastro)
        {
            var motorista = (Motorista)dadosCadastro;
            if (!Validar(motorista)) throw exception;

            motorista = Enriquecer(motorista);

            var entrada = (Repositorio.Entidade.Motorista)Converte.Transformar(motorista);

            Repositorio.Inserir(entrada);
        }

        public override IEnumerable<IDTO> Consultar(IFiltro ordenarPor)
        {
            var motoristas = Repositorio.Consultar();

            var saida = Converte.Transformar(motoristas);

            return OrdenarPor(saida, ordenarPor);

        }

        public override void Editar(IDTO dadosCadastro)
        {
            var motorista = (Motorista)dadosCadastro;

            if (!Validar(motorista)) throw exception;

            motorista = Enriquecer(motorista);

            var entrada = (Repositorio.Entidade.Motorista)Converte.Transformar(motorista);

            Repositorio.Editar(entrada);
        }

        public override void Excluir(int id)
        {
            if (!ValidarChave(id)) throw exception;

            Repositorio.Excluir(id);
        }
        
        protected override IEnumerable<Motorista> OrdenarPor(IEnumerable<Motorista> dadosCadastro, IFiltro filtroConsulta)
        {
            if (filtroConsulta.GetType() != typeof(FiltroMotorista)) throw new Exception("Filtro de ordenação da pesquisa está inválido");
            var filtro = (FiltroMotorista)filtroConsulta;

            var param = filtro.OrdenarMotoristaPor.ToString();
            var infoPropriedade = typeof(Motorista).GetProperty(param);

            return dadosCadastro.OrderBy(d => infoPropriedade.GetValue(d, null));
        }

        protected override Motorista Enriquecer(Motorista dadosCadastro)
        {
            var coordenadas = ObterCoordenadas(dadosCadastro.Endereco);
            dadosCadastro.Endereco.InformarCoordenadas(coordenadas.Latitude, coordenadas.Longitude);
            return dadosCadastro;
        }

        protected string FormatarEnderecoEmLinha(Endereco endereco)
        {
            return $"{endereco.Logradouro}, {endereco.Numero} {endereco.Bairro} {endereco.Cidade} - {endereco.Estado} CEP {endereco.CEP}";
        }

        protected Coordenadas ObterCoordenadas(Endereco endereco)
        {
            var parametro = FormatarEnderecoEmLinha(endereco);
            return ServicoGoogleMaps.ObterCoordenadas(parametro);
        }
        
        protected ITransformacao<Motorista> converte;
        protected ITransformacao<Motorista> Converte {
            get => converte ?? new TransformacaoMotorista();
            set => converte = value;
        }

        protected IRepositorio<Repositorio.Entidade.Motorista> repositorio;
        public IRepositorio<Repositorio.Entidade.Motorista> Repositorio
        {
            get => repositorio ?? new MotoristaRepositorio();
            set => repositorio = value;
        }

    }
}
