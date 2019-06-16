using AutoMapper;
using AutoMapper.Mappers;
using Motoristas.Core.States;

namespace Motoristas.Core.Mappers
{
    internal static class StateMapperExtensions
    {
        private static readonly IMapper Mapper;

        static StateMapperExtensions()
        {
            var config = new MapperConfiguration(c =>
                                                 {
                                                     c.AddConditionalObjectMapper().Where((s, d) => s.Name == $"{d.Name}State");
                                                     c.AddConditionalObjectMapper().Where((s, d) => $"{s.Name}State" == d.Name);
                                                 });
            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

        #region Endereço

        public static Endereco ToDomain(this EnderecoState state)
        {
            return Mapper.Map<Endereco>(state);
        }

        public static EnderecoState ToState(this Endereco model)
        {
            return Mapper.Map<EnderecoState>(model);
        }

        #endregion

        #region Veiculo 
        public static Veiculo ToDomain(this VeiculoState state)
        {
            return Mapper.Map<Veiculo>(state);
        }

        public static VeiculoState ToState(this Veiculo model)
        {
            return Mapper.Map<VeiculoState>(model);
        }

        #endregion

        #region Motorista

        public static Motorista ToDomain(this MotoristaState state)
        {
            return Mapper.Map<Motorista>(state);
        }

        public static MotoristaState ToState(this Motorista model)
        {
            return Mapper.Map<MotoristaState>(model);
        }

        #endregion

        #region Coordenadas

        public static Coordenadas ToDomain(this CoordenadasState state)
        {
            return Mapper.Map<Coordenadas>(state);
        }

        public static CoordenadasState ToState(this Coordenadas model)
        {
            return Mapper.Map<CoordenadasState>(model);
        }

        #endregion

    }
}