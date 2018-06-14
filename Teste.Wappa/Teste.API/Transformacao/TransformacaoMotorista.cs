using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.API.Contratos;
using Teste.Implementacao.Inteface;

namespace Teste.API.Transformacao
{
    public class TransformacaoMotorista : Transformacao<Motorista>
    {
        public override IDTO Transformar(Motorista contrato)
        {
            var retorno = (Motorista)contrato;
            var endereco = retorno.Endereco;
            var carro = retorno.Carro;
            var motorista = new Implementacao.Builder.MotoristaBuilder();

            return motorista.Novo(retorno.Id, retorno.Nome, retorno.Sobrenome)
                     .ComCarro(carro.Id, carro.IdMarca, carro.Marca, carro.IdModelo, carro.Modelo, carro.Placa)
                     .ComEndereco(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP, endereco.Latitude, endereco.Longitude)
                     .Criar();
        }

        public override Motorista Transformar(IDTO implementacao)
        {
            var retorno = (Implementacao.DTO.Motorista)implementacao;
            var endereco = retorno.Endereco;
            var carro = retorno.Carro;
            var motorista = new Builder.MotoristaBuilder();

            return motorista.Novo(retorno.Id, retorno.Nome, retorno.Sobrenome)
                     .ComCarro(carro.Id, carro.IdMarca, carro.Marca, carro.IdModelo, carro.Modelo, carro.Placa)
                     .ComEndereco(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP, endereco.Latitude, endereco.Longitude)
                     .Criar();
        }
    }
}
