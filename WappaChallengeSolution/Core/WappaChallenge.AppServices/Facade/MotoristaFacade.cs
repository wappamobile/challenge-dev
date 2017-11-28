using System.Collections.Generic;
using WappaChallenge.AppServices.Adapters;
using WappaChallenge.AppServices.Facade.Interfaces;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Facade
{
    public class MotoristaFacade : IMotoristaFacade
    {
        private readonly IMotoristaRepositorio _motoristaRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly ICoordenadaGeograficaRepositorio _coordenadaGeograficaRepositorio;

        public MotoristaFacade(IMotoristaRepositorio motoristaRepositorio,
            IVeiculoRepositorio veiculoRepositorio,
            IEnderecoRepositorio enderecoRepositorio,
            ICoordenadaGeograficaRepositorio coordenadaGeograficaRepositorio)
        {
            this._motoristaRepositorio = motoristaRepositorio;
            this._veiculoRepositorio = veiculoRepositorio;
            this._enderecoRepositorio = enderecoRepositorio;
            this._coordenadaGeograficaRepositorio = coordenadaGeograficaRepositorio;
        }


        public MotoristaDTO CadastrarMotorista(MotoristaDTO dto)
        {
            Motorista motorista = dto.ParaObjetoDeDominio();
            var coo = new GoogleMapsAPIFacade().ObterCoordenadasGeograficas(dto.Endereco).Result;

            motorista.Endereco.CoordenadaGeografica = new CoordenadaGeografica(coo.Latitude, coo.Longitude);

            _veiculoRepositorio.Cadastrar(motorista.Veiculo);
            _coordenadaGeograficaRepositorio.Cadastrar(motorista.Endereco.CoordenadaGeografica);
            _enderecoRepositorio.Cadastrar(motorista.Endereco);
            _motoristaRepositorio.Cadastrar(motorista);

            return motorista.ParaDTO();
        }

        public ICollection<MotoristaDTO> ObterTodosOrdenadoPorPrimeiroNome()
        {
            IEnumerable<Motorista> motoristas = this._motoristaRepositorio.ObterTodosOrdenadoPorPrimeiroNome();            
            return motoristas.ParaListaDeDTO();
        }

        public ICollection<MotoristaDTO> ObterTodosOrdenadoPorUltimoNome()
        {
            IEnumerable<Motorista> motoristas = this._motoristaRepositorio.ObterTodosOrdenadoPorUltimoNome();
            return motoristas.ParaListaDeDTO();
        }

        public MotoristaDTO AtualizarMotorista(MotoristaDTO dto)
        {
            Motorista motorista = dto.ParaObjetoDeDominio();
            var coo = new GoogleMapsAPIFacade().ObterCoordenadasGeograficas(dto.Endereco);

            _veiculoRepositorio.Atualizar(motorista.Veiculo);
            _enderecoRepositorio.Atualizar(motorista.Endereco);
            motorista = _motoristaRepositorio.Atualizar(motorista);

            return motorista.ParaDTO();
        }

        public void ExcluirMotorista(int id)
        {
            this._motoristaRepositorio.Excluir(id);
        }
    }
}
