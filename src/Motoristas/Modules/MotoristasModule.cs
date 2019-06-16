using MediatR;
using Motoristas.Handlers.Commands;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoristas.Modules
{
    public static class BasePathMapping
    {
        static BasePathMapping()
        {
            BasePath = string.Empty;
        }

        public static string BasePath { get; set; }
    }

    public class MotoristasModule : NancyModule
    {
        private readonly IMediator _mediator;
        private readonly string _basePathMapping = BasePathMapping.BasePath;

        public MotoristasModule(IMediator mediator) : base("/motoristas")
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            //TODO: Implement Athentication !
            //this.RequiresAuthentication();

            Post("", async p =>
            {
                var cmd = this.Bind<RegistrarPerfilMotorista>();

                var response = await mediator.Send(cmd);
                var result = CreatePerfilResponse(response.PerfilId);
                return Negotiate.WithStatusCode(HttpStatusCode.Created)
                                .WithHeader("Location", Flurl.Url.Combine(BasePathMapping.BasePath, ModulePath, response.PerfilId))
                                .WithModel(result);
            });

            Put("/{perfilId}/remover-cadastro", async p =>
            {
                var cmd = new RemoverPerfilMotorista(p.perfilId);
                await mediator.Send(cmd);
                return Negotiate.WithStatusCode(HttpStatusCode.SeeOther)
                                .WithHeader("Location", Flurl.Url.Combine(BasePathMapping.BasePath, ModulePath, cmd.PerfilId));
            });

            Get("/{perfilId}", async p =>
            {
                var cmd = new ObterPerfilMotorista(p.perfilId);
                await _mediator.Send(cmd);
                var result = CreatePerfilResponse(cmd.PerfilId);
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                                .WithModel(result);
            });

            Get(_basePathMapping, async p =>
            {
                var cmd = new PerfilMotoristaQuery { Sort = Request.Query["sort"] };
                var response = await _mediator.Send(cmd);
                var result = new
                {
                    Href = $"{Request.Path}",
                    Veiculos = response.Lista
                                .Select(v => new
                                {
                                    v.Id,
                                    v.DataCriacao,
                                    v.DataUltimaAtualizacao,
                                    NomeMotorista = v.Motorista.Nome,
                                    UltimoNomeMotorista = v.Motorista.UtimoNome,
                                    VeiculoMarca = v.Motorista.Veiculo.Marca,
                                    VeiculoModelo = v.Motorista.Veiculo.Modelo,
                                    VeiculoPlaca = v.Motorista.Veiculo.Placa,
                                    EnderecoLogradouro = v.Motorista.Endereco.Logradouro,
                                    EnderecoBairro = v.Motorista.Endereco.Bairro,
                                    EnderecoCidade = v.Motorista.Endereco.Cidade,
                                    EnderecoUf = v.Motorista.Endereco.Uf,
                                    EnderecoCep = v.Motorista.Endereco.Cep,
                                    EnderecoDescricao = v.Motorista.Endereco.Descricao,
                                    EnderecoNumero = v.Motorista.Endereco.Numero,
                                    EnderecoComplemento = v.Motorista.Endereco.Complemento,
                                    Links = new[]
                                    {
                                        new
                                        {
                                            Relative = "perfil",
                                            Href = $"{_basePathMapping}{ModulePath}/{v.Id}"
                                        }
                                    }
                                })
                };
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                                .WithModel(result);
            });

        }


        private object CreatePerfilResponse(string perfilId)
        {
            var result = new
            {
                Href = Request.Path,
                PerfilId = perfilId,
            };
            return result;
        }
    }
}
