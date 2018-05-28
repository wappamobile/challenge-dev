using Flunt.Notifications;
using System.Linq;
using Wappa.Dominio.Comando.Motorista;
using Wappa.Dominio.Repositorio;
using Wappa.Dominio.Resultado;
using Wappa.Dominio.Servico;

namespace Wappa.Dominio.Manipulador.Motorista
{
    public class ManipularMotorista : Notifiable,
                IManipulador<AlterarMotoristaComando>,
                IManipulador<CriarMotoristaComando>,
                IManipulador<ExcluirMotoristaComando>
    {
        private readonly IMotoristaRepositorio _motoristaRepositorio;
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly IGoogleMap _googleMapRepositorio;

        public ManipularMotorista(IMotoristaRepositorio motoristaRepositorio,
                                  ICarroRepositorio carroRepositorio,
                                  IEnderecoRepositorio enderecoRepositorio,
                                  IGoogleMap googleMapRepositorio)
        {
            this._motoristaRepositorio = motoristaRepositorio;
            this._carroRepositorio = carroRepositorio;
            this._enderecoRepositorio = enderecoRepositorio;
            this._googleMapRepositorio = googleMapRepositorio;
        }

        public IResultadoComando Manipular(ExcluirMotoristaComando comando)
        {
            comando.Validar();

            if (Invalid)
            {
                AddNotifications(comando);
                return new ResultadoComando(false, "Não foi possível excluir o motorista.", Notifications);
            }

            var motorista = _motoristaRepositorio.Obter(x => x.Id == comando.Id);

            if (motorista == null)
            {
                return new ResultadoComando(false, "Motorista não encontrado.", Notifications);
            }

            //perfistir o motorista no banco de dados.
            _motoristaRepositorio.Excluir(motorista, false);

            if (motorista.ListaCarro.Any())
            {
                _carroRepositorio.ExcluirLista(motorista.ListaCarro, false);
            }

            if (motorista.ListaEndereco.Any())
            {
                _enderecoRepositorio.ExcluirLista(motorista.ListaEndereco, false);
            }

            _carroRepositorio.Commit();
            _enderecoRepositorio.Commit();
            _motoristaRepositorio.Commit();

            return new ResultadoComando(true, "Motorista excluído com sucesso!", new
            {
                Name = motorista.Nome,
                SobreNome = motorista.Sobrenome
            });
        }

        public IResultadoComando Manipular(CriarMotoristaComando comando)
        {
            comando.Validar();

            if (Invalid)
            {
                AddNotifications(comando);
                return new ResultadoComando(false, "Não foi possível cadastrar o motorista.", Notifications);
            }

            //criar entidade
            var motorista = new Entidade.Motorista
            {
                Nome = comando.Nome,
                Sobrenome = comando.SobreNome,
                ListaCarro = comando.CarroLista,
                ListaEndereco = comando.EnderecoLista
            };

            motorista.ListaEndereco.ToList().ForEach(e =>
            {
                var localidade = _googleMapRepositorio.ObterLocalidade(e).results.SingleOrDefault()?.geometry?.location;

                if (localidade != null)
                {
                    e.Latitude = localidade.lat;
                    e.Longitude = localidade.lng;
                }

            });

            //perfistir o motorista no banco de dados.
            _motoristaRepositorio.Inserir(motorista, true);

            return new ResultadoComando(true, "Motorista Cadastrado com sucesso!", new
            {
                Name = comando.Nome.ToString(),
                SobreNome = comando.SobreNome
            });
        }

        public IResultadoComando Manipular(AlterarMotoristaComando comando)
        {
            comando.Validar();

            if (Invalid)
            {
                AddNotifications(comando);
                return new ResultadoComando(false, "Não foi possível alterar o motorista.", Notifications);
            }

            var motorista = new Entidade.Motorista
            {
                Id = comando.Id,
                Nome = comando.Nome,
                Sobrenome = comando.SobreNome
            };

            _motoristaRepositorio.Atualizar(motorista, false);

            if (comando.CarroLista.Any())
            {
                _carroRepositorio.AtualizarLista(comando.CarroLista, false);
            }

            if (comando.EnderecoLista.Any())
            {
                _enderecoRepositorio.AtualizarLista(comando.EnderecoLista, false);
            }


            motorista.ListaEndereco.ToList().ForEach(e =>
            {
                var localidade = _googleMapRepositorio.ObterLocalidade(e).results.SingleOrDefault()?.geometry?.location;

                if (localidade != null)
                {
                    e.Latitude = localidade.lat;
                    e.Longitude = localidade.lng;
                }

            });

            _carroRepositorio.Commit();
            _enderecoRepositorio.Commit();
            _motoristaRepositorio.Commit();

            return new ResultadoComando(true, "Motorista alterado com sucesso!", new
            {
                Name = comando.Nome.ToString(),
                SobreNome = comando.SobreNome
            });
        }
    }
}