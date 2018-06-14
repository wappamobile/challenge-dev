using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.DTO;
using Teste.Repositorio.Interface;

namespace Teste.Implementacao.Transformacao
{
    public class TransformacaoMotorista : Transformacao<Motorista>
    {
        public override Motorista Transformar(IEntidade entidade)
        {
            var retorno = (Repositorio.Entidade.Motorista)entidade;
            var endereco = retorno.Endereco;
            var carro = retorno.Carro;
            var motorista = new Builder.MotoristaBuilder();

            return motorista.Novo(retorno.ID, retorno.Nome, retorno.Sobrenome)
                     .ComCarro(carro.Id, carro.Modelo.Marca.Id, carro.Modelo.Marca.Descricao, carro.Modelo.Id, carro.Modelo.Descricao, carro.Placa)
                     .ComEndereco(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP, endereco.Latitude, endereco.Longitude)
                     .Criar();
        }

        public override IEntidade Transformar(Motorista implementacao)
        {
            var endereco = implementacao.Endereco;
            var carro = implementacao.Carro;
            var motorista = new Repositorio.Builder.MotoristaBuilder();

            return motorista.Novo(implementacao.Id, implementacao.Nome, implementacao.Sobrenome)
                            .ComCarro(carro.Id, carro.IdModelo, carro.Modelo, carro.Placa)
                            .ComEndereco(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP, (double)endereco.Latitude, (double)endereco.Longitude)
                            .Criar();
        }
    }
}
