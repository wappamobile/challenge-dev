using WappaChallenge.AppServices.Adapters;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Facade
{
    public class MotoristaFacade
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


        public void CadastrarMotorista(MotoristaDTO dto)
        {
            Motorista motorista = dto.ParaObjetoDeDominio();
            var coo = new GoogleMapsAPIFacade().ObterCoordenadasGeograficas(dto.Endereco);

            _veiculoRepositorio.Cadastrar(motorista.Veiculo);
            

            _enderecoRepositorio.Cadastrar(motorista.Endereco);


            _motoristaRepositorio.Cadastrar(motorista);
        }
    }
}
