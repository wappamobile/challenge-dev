using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Dominio.Comando.Motorista;
using Wappa.Dominio.Entidade;
using Wappa.Dominio.Manipulador.Motorista;
using Wappa.Dominio.Repositorio;
using Wappa.Dominio.Servico;
using Wappa.ViewModel.Motorista.Comando;

namespace Wappa.API.Controllers
{
    public class MotoristaController : BaseController
    {
        private readonly IMotoristaRepositorio _motoristaRepositorio;
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly IGoogleMap _googleMapRepositorio;
        private readonly ManipularMotorista manipulador;

        public MotoristaController(IMotoristaRepositorio motoristaRepositorio,
                                   ICarroRepositorio carroRepositorio,
                                   IEnderecoRepositorio enderecoRepositorio,
                                   IGoogleMap googleMapRepositorio)
        {
            this._motoristaRepositorio = motoristaRepositorio;
            this._carroRepositorio = carroRepositorio;
            this._enderecoRepositorio = enderecoRepositorio;
            this._googleMapRepositorio = googleMapRepositorio;

            manipulador = new ManipularMotorista(_motoristaRepositorio,
                                                 _carroRepositorio,
                                                 _enderecoRepositorio,
                                                 _googleMapRepositorio);
        }

        /// <summary>
        /// Método para criar um novo motorista.
        /// </summary>
        /// <param name="CriarMotorista"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/motorista")]
        public async Task<IActionResult> Post([FromBody]CriarMotoristaVM comando)
        {
            var criarMotorista = new CriarMotoristaComando(comando.Nome, comando.SobreNome);

            comando.EnderecoLista.ToList().ForEach(e => criarMotorista.AdicionarEndereco(new Endereco
            {
                Cep = e.Cep,
                Complemento = e.Complemento,
                Numero = e.Numero,
                Logradouro = e.Logradouro
            }));

            comando.CarroLista.ToList().ForEach(c => criarMotorista.AdicionarCarro(new Carro
            {
                Cor = c.Cor,
                Lugar = c.Lugar,
                Mala = c.Mala,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Placa = c.Placa
            }));

            var resultado = manipulador.Manipular(criarMotorista);

            return await Response(resultado, manipulador.Notifications);
        }

        /// <summary>
        /// Método para alterar um  motorista.
        /// </summary>
        /// <param name="AlterarMotorista"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/motorista")]
        public async Task<IActionResult> Put([FromBody]AlterarMotoristaVM comando)
        {
            var alterarMotorista = new AlterarMotoristaComando(comando.Id, comando.Nome, comando.SobreNome);

            comando.EnderecoLista.ToList().ForEach(e => alterarMotorista.AdicionarEndereco(new Endereco
            {
                Id = e.Id,
                IdMotorista = comando.Id,
                Cep = e.Cep,
                Complemento = e.Complemento,
                Numero = e.Numero,
                Logradouro = e.Logradouro
            }));

            comando.CarroLista.ToList().ForEach(c => alterarMotorista.AdicionarCarro(new Carro
            {
                Id = c.Id,
                IdMotorista = comando.Id,
                Cor = c.Cor,
                Lugar = c.Lugar,
                Mala = c.Mala,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Placa = c.Placa
            }));

            var resultado = manipulador.Manipular(alterarMotorista);

            return await Response(resultado, manipulador.Notifications);
        }

        /// <summary>
        /// Método para obter todos os  motoristas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/motorista")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> Get()
        {
            var motoristas = _motoristaRepositorio.ObterTodos();

            return await Response(motoristas);
        }

        /// <summary>
        ///  Método para excluir motorista.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/motorista/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var excluirMotorista = new ExcluirMotoristaComando(id);

            var resultado = manipulador.Manipular(excluirMotorista);

            return await Response(resultado, manipulador.Notifications);
        }
    }
}