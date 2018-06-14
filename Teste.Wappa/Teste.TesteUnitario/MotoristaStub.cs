using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.DTO;

namespace Teste.TesteUnitario
{
    public static class MotoristaStub
    {
        public static Motorista ObterMotoristaParaCadastroValido()
        {
            var builder = new Implementacao.Builder.MotoristaBuilder();
            return builder.Novo(null, "Aurelio", "Oliveira")
                          .ComCarro(null, null, null, 1, null, "ABC1234")
                          .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, null, null)
                          .Criar();
        }

        public static Motorista ObterMotoristaParaCadastroSobrenomeInvalido()
        {
            var builder = new Implementacao.Builder.MotoristaBuilder();
            return builder.Novo(null, "Aurelio", null)
                          .ComCarro(null, null, null, 1, null, "ABC1234")
                          .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, null, null)
                          .Criar();
        }

        public static Motorista ObterMotoristaParaCadastroLogradouroInvalido()
        {
            var builder = new Implementacao.Builder.MotoristaBuilder();
            return builder.Novo(null, "Aurelio", "Oliveira")
                          .ComCarro(null, null, null, 1, null, "ABC1234")
                          .ComEndereco(null, null, "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, null, null)
                          .Criar();
        }

        public static Motorista ObterMotoristaParaCadastroPlacaInvalida()
        {
            var builder = new Implementacao.Builder.MotoristaBuilder();
            return builder.Novo(null, "Aurelio", "Oliveira")
                          .ComCarro(null, null, null, 1, null, null)
                          .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, null, null)
                          .Criar();
        }


        public static Repositorio.Entidade.Motorista ObterRepositorioMotoristaParaCadastroValido()
        {
            var builder = new Repositorio.Builder.MotoristaBuilder();
            return builder.Novo(null, "Aurelio", "Oliveira")
                          .ComCarro(null, 1, null, "ABC1234")
                          .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, -23.5602317, -46.667542300000008)
                          .Criar();
        }

        public static double LatitudeCadastro
        {
            get => -23.5602317;
        }

        public static double LongitudeCadastro
        {
            get => -46.667542300000008;
        }

        public static IEnumerable<Repositorio.Entidade.Motorista> ObterRepositorioListaMotoristasSemOrdenar()
        {
            var motoristas = new List<Repositorio.Entidade.Motorista>();

            motoristas.Add(ObterMotorista1());
            motoristas.Add(ObterMotorista2());
            motoristas.Add(ObterMotorista3());

            return motoristas;
        }

        private static Repositorio.Entidade.Motorista ObterMotorista3()
        {
            var builder = new Repositorio.Builder.MotoristaBuilder();

            return builder.Novo(1, "Eduardo", "Antunes")
                .ComCarro(1, 1, string.Empty, 1, string.Empty, "BBB999")
                .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, -23.5602317, -46.667542300000008)
                .Criar();
        }

        private static Repositorio.Entidade.Motorista ObterMotorista2()
        {
            var builder = new Repositorio.Builder.MotoristaBuilder();

            return builder.Novo(2, "Joao", "Silva")
                .ComCarro(2, 2, string.Empty, 2, string.Empty, "AAA1234")
                .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, -23.5602317, -46.667542300000008)
                .Criar();
        }

        private static Repositorio.Entidade.Motorista ObterMotorista1()
        {
            var builder = new Repositorio.Builder.MotoristaBuilder();

            return builder.Novo(2, "Fabio", "Oliveira")
                .ComCarro(3, 3, string.Empty, 3, string.Empty, "DDD5478")
                .ComEndereco(null, "Rua da Consolacao", "3161", null, "Cerqueira César", "São Paulo", "SP", 0400385, -23.5602317, -46.667542300000008)
                .Criar();
        }
    }
}
